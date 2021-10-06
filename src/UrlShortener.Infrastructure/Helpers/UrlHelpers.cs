using System;

namespace UrlShortener.Infrastructure.Helpers
{
    /// <summary>
    /// Helper methods for url
    /// </summary>
    public static class UrlHelpers
    {
        /// <summary>
        /// Converts unit time to <see cref="DateTime"/>
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns>The unix time converted to <see cref="DateTime"/></returns>
        public static DateTime ConvertUnixTimeToDateTime(long unixTimeStamp)
        {
            AssertArgument.NotNull(unixTimeStamp, nameof(unixTimeStamp));

            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var result = dateTime.AddSeconds(unixTimeStamp);
            return result;
        }
    }
}
