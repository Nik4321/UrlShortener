using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Data.Repositories;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.Infrastructure.Exceptions;
using Xunit;

namespace UrlShortener.Services.Tests.Unit
{
    public class UrlServiceTests
    {
        private const string TestLongUrl = "https://google.com/";

        private readonly IFixture fixture;
        private readonly Mock<IUrlRepository> urlRepository;
        private readonly IUrlService sut;

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

            result.Should().Equals(url);
        }

        [Fact]
        public async Task ShortenUrl_ShouldReturnCorrectUrlObject_WhenCalledWithValidUrl()
        {
            // Act
            var result = await this.sut.ShortenUrl(TestLongUrl);

            // Assert
            result.Should()
                .Match<Url>(x => x.LongUrl == TestLongUrl);
        }

        [Fact]
        public async Task ShortenUrl_ShouldReturnUrlObjectWithExpireDate_WhenCalledWithValidUrlAndExpireDate()
        {
            // Arrange
            // 03/08/2020 @ 6:23pm - 1583691810
            var seconds = 1583691810;
            var expectedExpirationDate = DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

            // Act
            var result = await this.sut.ShortenUrl(TestLongUrl, seconds);

            // Assert
            Assert.Equal(result.ExpirationDate, expectedExpirationDate);
        }

        [Fact]
        public void ShortenUrl_ShouldThrowInvalidUrlException_WhenCalledWithInvalidUrl()
        {
            // Act
            var result = this.sut.Invoking(x => x.ShortenUrl(It.IsAny<string>()).GetAwaiter().GetResult());

            // Assert
            result.Should()
                .Throw<InvalidUrlException>()
                .WithMessage(ExceptionMessagesConstants.InvalidUrlExceptionMessage);
        }

        #endregion

        #region GetUrlTests

        [Fact]
        public async Task GetUrl_ShouldReturnCorrectType_WhenCalledWithExistingEntity()
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
            result.Should().NotBeNull().And.BeOfType<Url>();
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
            result.Should().BeNull();
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
            result.Should().Be(expectedResult);
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
            result.Should().BeFalse();
        }

        #endregion
    }
}
