using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UrlShortener.Data;
using UrlShortener.Data.Models;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.Repositories;
using UrlShortener.Services.Tests.Base;
using Xunit;

namespace UrlShortener.Services.Tests.Unit
{
    public class UrlTest : BaseTest, IDisposable
    {
        private const string TestLongUrl = "https://google.com/";

        private readonly Mock<UrlRepository> urlRepository;
        private readonly IUrlService urlService;


        public UrlTest()
        {
            this.BaseSetUp();
            this.urlRepository = new Mock<UrlRepository>(this.db);
            this.urlService = new UrlService(this.urlRepository.Object);
            AddFakeUrlsToDb(this.db);
        }

        public void Dispose()
        {
            this.db.Database.EnsureDeleted();
        }

        #region ShortenUrlTests

        [Fact]
        public async Task ShortenUrl_WhenCalledWithValidUrl_ReturnsObjectOfTypeUrl()
        {
            // Act
            var result = await this.urlService.ShortenUrl(TestLongUrl);

            // Assert
            result.Should().BeOfType<Url>();
        }

        [Fact]
        public async Task ShortenUrl_WhenCalledWithValidUrl_ReturnsCorrectUrlObject()
        {
            // Act
            var result = await this.urlService.ShortenUrl(TestLongUrl);

            // Assert
            result.Should()
                .Match<Url>(x => x.LongUrl == TestLongUrl);
        }

        [Fact]
        public async Task ShortenUrl_WhenCalledWithValidUrlAndExpireDate_ReturnsUrlObjectWithExpireDate()
        {
            // Arrange
            // 03/08/2020 @ 6:23pm - 1583691810
            var expectedExpirationDate = DateTimeOffset.FromUnixTimeSeconds(1583691810).DateTime;

            // Act
            var result = await this.urlService.ShortenUrl(TestLongUrl, 1583691810);

            // Assert
            Assert.Equal(result.ExpirationDate, expectedExpirationDate);
        }

        [Fact]
        public void ShortenUrl_WhenCalledWithInvalidUrl_ThrowsInvalidUrlException()
        {
            // Act
            var result = this.urlService.Invoking(x => x.ShortenUrl(It.IsAny<string>()).GetAwaiter().GetResult());

            // Assert
            result.Should()
                .Throw<InvalidUrlException>()
                .WithMessage(ExceptionMessagesConstants.InvalidUrlExceptionMessage);
        }

        #endregion

        #region GetUrlByShortUrlTests

        [Fact]
        public async Task GetUrlByShortUrl_WhenCalledWithExistingEntity_ReturnsCorrectType()
        {
            // Act
            var result = await this.urlService.GetUrlByShortUrl("197dec01");

            // Assert
            result.Should().NotBeNull().And.BeOfType<Url>();
        }

        [Fact]
        public async Task GetUrlByShortUrl_WhenCalledWithNonExistingEntity_ReturnsNull()
        {
            // Act
            var result = await this.urlService.GetUrlByShortUrl(It.IsAny<string>());

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region  HasUrlExpiredTests

        [Theory]
        [InlineData(5, false)]
        [InlineData(-5, true)]
        public void HasUrlExpired_WithExpirationDate_ReturnsCorrectBoolean(int hoursToAddToExpiration, bool expectedResult)
        {
            // Arrange
            var url = new Url
            {
                ShortUrl = "197dec01",
                LongUrl = TestLongUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(hoursToAddToExpiration)
            };

            // Act
            var result = this.urlService.HasUrlExpired(url);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void HasUrlExpired_WithNoExpiredDate_ReturnsTrue()
        {
            // Arrange
            var url = new Url
            {
                ShortUrl = "197dec01",
                LongUrl = TestLongUrl,
                ExpirationDate = null
            };

            // Act
            var result = this.urlService.HasUrlExpired(url);

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Helper methods

        private static void AddFakeUrlsToDb(UrlShortenerDbContext db)
        {
            IList<Url> urls = new List<Url>
            {
                new Url
                {
                    ShortUrl = "197dec01",
                    LongUrl = TestLongUrl,
                    ExpirationDate = DateTime.UtcNow.AddHours(1)
                }
            };

            db.Urls.AddRange(urls);
            db.SaveChanges();
        }

        #endregion
    }
}
