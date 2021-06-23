using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Data.Repositories;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.Infrastructure.Extensions;
using UrlShortener.Infrastructure.Helpers;

namespace UrlShortener.Services
{
    /// <inheritdoc/>
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository urlRepository;

        /// <summary>
        /// Creates an instance of <see cref="UrlService"/>
        /// </summary>
        /// <param name="urlRepository">The repository used for url persistance management</param>
        public UrlService(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
        }

        /// <inheritdoc/>
        public async Task<Url> ShortenUrl(string longUrl, long? expireDate = null)
        {
            var isLongUrlValid = longUrl.IsValidUrl();
            if (!isLongUrlValid)
            {
                throw new InvalidUrlException(ExceptionMessagesConstants.InvalidUrlExceptionMessage);
            }

            var shortUrl = await this.GenerateShortUrl();
            var url = new Url
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl
            };

            if (expireDate.HasValue && expireDate.Value > 0)
            {
                url.ExpirationDate = UrlHelpers.ConvertUnixTimeToDateTime(expireDate.Value);
            }

            await this.urlRepository.AddAsync(url);
            return url;
        }

        /// <inheritdoc/>
        public Task<Url> GetUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                throw new ArgumentNullException(nameof(shortUrl));
            }

            return this.urlRepository.GetByShortUrl(shortUrl);
        }
        
        /// <inheritdoc/>
        public bool HasUrlExpired(Url url)
        {
            if (!url.ExpirationDate.HasValue)
            {
                return false;
            }

            return url.ExpirationDate.Value < DateTime.UtcNow;
        }

        #region Helper Methods

        /// <summary>
        /// Used to generate short url for <see cref="Url"/> entity
        /// </summary>
        /// <returns>A task with the generated short url</returns>
        private async Task<string> GenerateShortUrl()
        {
            while (true)
            {
                var url = Guid.NewGuid().ToString().Substring(0, 8);

                var exists = await this.urlRepository.ExistsAsync(x => x.ShortUrl == url);
                if (!exists)
                {
                    return url;
                }
            }
        }

        #endregion
    }
}
