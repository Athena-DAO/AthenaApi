using Microsoft.Net.Http.Headers;
using System;
using System.Diagnostics;
using System.IO;

namespace CommandAndControlWebApi.Helpers
{
    public class MultipartRequestHelper
    {
        public static bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType)
                && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string GetBoundry(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundry = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if(string.IsNullOrWhiteSpace(boundry+""))
            {
                throw new InvalidDataException("Missing content-type boundry");
            }

            if(boundry.Length > lengthLimit)
            {
                throw new InvalidDataException("Multipart boundry exceeded");
            }

            return boundry+"";
        }

        public static bool HasFileContentDispostion(ContentDispositionHeaderValue contentDisposition)
        {
            return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && (!string.IsNullOrEmpty(contentDisposition.FileName + "") 
                    || !string.IsNullOrEmpty(contentDisposition.FileNameStar+ ""));
        }

        public static bool HasFormDataContentDispostion(ContentDispositionHeaderValue contentDisposition)
        {
            return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && string.IsNullOrEmpty(contentDisposition.FileName+"")
                && string.IsNullOrEmpty(contentDisposition.FileNameStar+"");
        }
    }
}
