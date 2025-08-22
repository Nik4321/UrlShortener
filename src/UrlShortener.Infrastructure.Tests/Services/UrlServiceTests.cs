using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Data.Repositories;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.Infrastructure.Services;
using Xunit;

namespace UrlShortener.Infrastructure.Tests.Services
{
    public class UrlServiceTests
    {
        private const string TestLongUrl = "https://google.com/";

        private readonly IFixture fixture;
        private readonly Mock<IUrlRepository> urlRepository;
        private readonly UrlService sut;

        public UrlServiceTests()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.urlRepository = fixture.Freeze<Mock<IUrlRepository>>();
            this.sut = fixture.Create<UrlService>();
        }

        [Fact]
        public void CheckConstructorArguments()
        {
            new GuardClauseAssertion(this.fixture).Verify(typeof(UrlService).GetConstructors());
        }

        #region ShortenUrlTests

        [Fact]
        public async Task ShortenUrl_ShouldReturnUrl_WhenCalledWithValidUrl()
        {
            // Arrange
            var shortUrl = "197dec01";
            var url = new Url
            {
                Id = 1,
                ShortUrl = shortUrl,
                LongUrl = TestLongUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(1),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            this.urlRepository
                .Setup(x => x.AddAsync(It.IsAny<Url>(), true))
                .ReturnsAsync(url);

            // Act
            var result = await this.sut.ShortenUrl(TestLongUrl);

            // Assert
            this.urlRepository
                .Verify(x => x.AddAsync(It.IsAny<Url>(), true));

            result.ShouldBe(url);
        }

        [Fact]
        public async Task ShortenUrl_ShouldSaveLongUrl_WhenCalledWithValidUrl()
        {
            // Act
            var result = await this.sut.ShortenUrl(TestLongUrl);

            // Assert
            this.urlRepository
                .Verify(x => x.AddAsync(It.Is<Url>(u => u.LongUrl == TestLongUrl), true),
                Times.Once);
        }

        [Fact]
        public async Task ShortenUrl_ShouldSaveExpireDate_WhenCalledWithValidUrlAndExpireDate()
        {
            // Arrange
            // 03/08/2020 @ 6:23pm - 1583691810
            var seconds = 1583691810;
            var expectedExpirationDate = DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

            // Act
            var result = await this.sut.ShortenUrl(TestLongUrl, seconds);

            // Assert
            this.urlRepository
                .Verify(x => x.AddAsync(It.Is<Url>(u => u.ExpirationDate == expectedExpirationDate), true),
                Times.Once);
        }

        [Fact]
        public void ShortenUrl_ShouldThrowInvalidUrlException_WhenCalledWithInvalidUrl()
        {
            // Arrange
            var url = fixture.Create<string>();

            // Act & Assert
            var ex = Should.Throw<InvalidUrlException>(() => this.sut.ShortenUrl(url).GetAwaiter().GetResult());

            ex.Message.ShouldBe(ExceptionMessagesConstants.InvalidUrlExceptionMessage);
        }

        #endregion

        #region GetUrlTests

        [Fact]
        public async Task GetUrl_ShouldReturnUrl_WhenCalledWithExistingEntity()
        {
            // Arrange
            var shortUrl = "197dec01";

            var url = new Url
            {
                Id = 1,
                ShortUrl = shortUrl,
                LongUrl = TestLongUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(1),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            this.urlRepository
                .Setup(x => x.GetByShortUrl(It.IsAny<string>()))
                .ReturnsAsync(url);

            // Act
            var result = await this.sut.GetUrl(shortUrl);

            // Assert
            result.ShouldBe(url);
        }

        [Fact]
        public async Task GetUrl_ShouldReturnNull_WhenCalledWithNonExistingEntity()
        {
            // Arrange
            this.urlRepository
                .Setup(x => x.GetByShortUrl(It.IsAny<string>()))
                .ReturnsAsync((Url)null);

            // Act
            var result = await this.sut.GetUrl("test");

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region  HasUrlExpiredTests

        [Theory]
        [InlineData(5, false)]
        [InlineData(-5, true)]
        public void HasUrlExpired_ShouldReturnCorrectBoolean_WithExpirationDate(int hoursToAddToExpiration, bool expectedResult)
        {
            // Arrange
            var shortUrl = "197dec01";
            var url = new Url
            {
                ShortUrl = shortUrl,
                LongUrl = TestLongUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(hoursToAddToExpiration)
            };

            // Act
            var result = this.sut.HasUrlExpired(url);

            // Assert
            result.ShouldBe(expectedResult);
        }

        [Fact]
        public void HasUrlExpired_ShouldReturnTrue_WithNoExpiredDate()
        {
            // Arrange
            var shortUrl = "197dec01";
            var url = new Url
            {
                ShortUrl = shortUrl,
                LongUrl = TestLongUrl,
                ExpirationDate = null
            };

            // Act
            var result = this.sut.HasUrlExpired(url);

            // Assert
            result.ShouldBeFalse();
        }

        #endregion
    }
}
