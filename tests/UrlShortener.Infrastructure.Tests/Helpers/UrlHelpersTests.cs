using System;
using UrlShortener.Infrastructure.Helpers;
using Xunit;

namespace UrlShortener.Infrastructure.Tests.Helpers
{
    public class UrlHelpersTests
    {
        [Fact]
        public void ConvertUnixTimeToDateTime_ShouldConvertTimeCorrectly()
        {
            // 03/08/2020 @ 6:23:30pm - 1583691810
            var expected = new DateTime(2020, 03, 08, 18, 23, 30, DateTimeKind.Utc);
            var unixTime = 1583691810;

            var actual = UrlHelpers.ConvertUnixTimeToDateTime(unixTime);

            Assert.Equal(expected, actual);
        }
    }
}
