using System;

namespace UrlShortener.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Validates that a string is a valid url
        /// </summary>
        /// <param name="value">The string being validated</param>
        /// <returns></returns>
        public static bool IsValidUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
