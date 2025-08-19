using UrlShortener.Infrastructure.Extensions;
using Xunit;

namespace UrlShortener.Infrastructure.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsValidUrl_ShouldReturnTrue_WhenUrlIsValidHttp()
        {
            var url = "http://google.com/";

            var actual = url.IsValidUrl();

            Assert.True(actual);
        }

        [Fact]
        public void IsValidUrl_ShouldReturnTrue_WhenUrlIsValidHttps()
        {
            var url = "https://google.com/";

            var actual = url.IsValidUrl();

            Assert.True(actual);
        }

        [Fact]
        public void IsValidUrl_ShouldReturnFalse_WhenUrlHasInvalidScheme()
        {
            var url = "www.google.com/";

            var actual = url.IsValidUrl();

            Assert.False(actual);
        }

        [Fact]
        public void IsValidUrl_ShouldReturnFalse_WhenUrlIsInvalid()
        {
            var url = "https:/google.com";

            var actual = url.IsValidUrl();

            Assert.False(actual);
        }
    }
}
