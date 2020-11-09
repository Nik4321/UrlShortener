using System;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Data.Models.Dtos.Authorize
{
    [ExcludeFromCodeCoverage]
    public class TokenResponse
    {
        public string UserEmail { get; set; }

        public string Type { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}