using System;

namespace UrlShortener.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
