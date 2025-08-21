using Microsoft.AspNetCore.WebUtilities;
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
        public HttpStatusCode Code { get; }

        /// <summary>
        /// The error code name
        /// </summary>
        public string CodeName => ReasonPhrases.GetReasonPhrase((int)Code);

        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; }
    }
}
