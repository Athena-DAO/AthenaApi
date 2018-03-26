using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using CommandAndControlWebApi.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Text;
using CommandAndControlWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/DataSets")]
    public class DataSetsController : Controller
    {
        private static readonly FormOptions defaultFormOptions = new FormOptions();

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if(!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest("Did not get multipart data");
            }

            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;
            string fileName = Guid.NewGuid().ToString().Trim();
            string fileExtension = "";

            var boundry = MultipartRequestHelper.GetBoundry(MediaTypeHeaderValue.Parse(Request.ContentType),
                defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundry, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while(section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDisposition = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if(hasContentDisposition)
                {
                    if(MultipartRequestHelper.HasFileContentDispostion(contentDisposition))
                    {
                        fileExtension = Path.GetExtension(HeaderUtilities.RemoveQuotes(contentDisposition.FileName) + "").Trim();
                        targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DataSets", fileName) + fileExtension;
                        using(var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDispostion(contentDisposition))
                    {
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            var value = await streamReader.ReadToEndAsync();
                            if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }
                            formAccumulator.Append(key + "", value);

                            if (formAccumulator.ValueCount > defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException("Form key limit exceeded");
                            }
                        }
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            var dataSet = new DataSetViewModel();
            var formValueProvider = new FormValueProvider(BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);
            var bindingSuccessful = await TryUpdateModelAsync(dataSet, prefix: "", valueProvider: formValueProvider);

            if(!bindingSuccessful)
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Ok("/DataSets/"+fileName+fileExtension);
        }


        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}