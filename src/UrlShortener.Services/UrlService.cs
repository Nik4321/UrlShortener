﻿using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.Infrastructure.Extensions;
using UrlShortener.Repositories;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository;
        }

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
                url.ExpirationDate = UnixTimeToDateTime(expireDate.Value);
            }

            await this.urlRepository.AddAsync(url);
            return url;
        }

        public Task<Url> GetUrlByShortUrl(string shortUrl) =>
            this.urlRepository.GetByShortUrl(shortUrl);

        public bool HasUrlExpired(Url url)
        {
            if (!url.ExpirationDate.HasValue)
            {
                return false;
            }

            return url.ExpirationDate.Value < DateTime.UtcNow;
        }

        #region Helper Methods

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

        private static DateTime UnixTimeToDateTime(long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var result = dateTime.AddSeconds(unixTimeStamp);
            return result;
        }

        #endregion
    }
}
