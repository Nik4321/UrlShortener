﻿using Newtonsoft.Json;

namespace UrlShortener.Models.Errors
{
    public class BaseResponseError
    {
        public BaseResponseError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public BaseResponseError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }

        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }
    }
}
