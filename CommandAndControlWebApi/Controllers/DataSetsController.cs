﻿using System;
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
using CommandAndControlWebApi.Models;
using CommandAndControlWebApi.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/DataSets")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class DataSetsController : Controller
    {
        private static readonly FormOptions defaultFormOptions = new FormOptions();
        private readonly DataCenterContext dataCenterContext;
        private readonly UserManager<IdentityUser> userManager;

        public DataSetsController(DataCenterContext dataCenterContext, UserManager<IdentityUser> userManager)
        {
            this.dataCenterContext = dataCenterContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<DataSetViewModel> Get()
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            var profileId = dataCenterContext.Profiles.Where(x => x.Id == _id).First().Id;
            List<DataSetViewModel> datasets = dataCenterContext.DataSets
                .Where(x => x.DataSetProfiles.Where(y => y.ProfileId == profileId).Count() > 0)
                .Select(x => new DataSetViewModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
            return datasets;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
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
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDisposition = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDisposition)
                {
                    if (MultipartRequestHelper.HasFileContentDispostion(contentDisposition))
                    {
                        fileExtension = Path.GetExtension(HeaderUtilities.RemoveQuotes(contentDisposition.FileName) + "").Trim();
                        targetFilePath = Path.Combine("DataSets", fileName) + fileExtension;
                        using (var targetStream = System.IO.File.Create(targetFilePath))
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
            if (bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var _id = Guid.Parse(userManager.GetUserId(User));
                    Profile profile = dataCenterContext.Profiles.Where(x => x.Id == _id).First();
                    DataSet myDataSet = new DataSet
                    {
                        Id = Guid.Parse(fileName),
                        Name = dataSet.Name,
                        Description = dataSet.Description,
                        URL = "/DataSets/" + fileName + fileExtension
                    };
                    ProfileDataSet profileDataSet = new ProfileDataSet
                    {
                        DataSet = myDataSet,
                        Profile = profile
                    };
                    dataCenterContext.Add(profileDataSet);
                    dataCenterContext.SaveChanges();
                }
            }

            return Ok("/DataSets/" + fileName + fileExtension);
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