using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;

namespace UrlShortener.Data.Models.Dtos.Errors
{
    /// <summary>
    /// A generic API error
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Constructs an <see cref="ApiError"/>
        /// </summary>
        /// <param name="code">The error code</param>
        /// <param name="message">The error message</param>
        public ApiError(HttpStatusCode code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty(Order = 1)]
        public HttpStatusCode Code { get; }

        /// <summary>
        /// The error code name
        /// </summary>
        [JsonProperty(Order = 2)]
        public string CodeName => ReasonPhrases.GetReasonPhrase((int)Code);

        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty(Order = 3)]
        public string Message { get; }
    }
}
