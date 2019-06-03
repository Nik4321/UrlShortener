using System;
using System.Linq;
using UrlShortener.Data;
using UrlShortener.Data.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDbContext db;

        public UrlService(UrlShortenerDbContext db)
        {
            this.db = db;
        }

        public Url ShortenUrl(string longUrl, long? expireDate = null)
        {
            var shortUrl = this.GenerateShortUrl();

            var url = new Url
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                CreatedOn = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(15)
            };

            if (expireDate.HasValue && expireDate.Value > 0)
            {
                url.ExpirationDate = UnixTimeToDateTime(expireDate.Value);
            }

            this.CreateUrl(url);
            return url;
        }

        public Url GetUrlByShortUrl(string shortUrl) =>
            this.db.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);

        public bool HasUrlExpired(Url url)
        {
            if (!url.ExpirationDate.HasValue)
            {
                return false;
            }

            return url.ExpirationDate.Value < DateTime.UtcNow;
        }

        #region Helper Methods

        private void CreateUrl(Url url)
        {
            this.db.Urls.Add(url);
            this.db.SaveChanges();
        }

        private string GenerateShortUrl()
        {
            while (true)
            {
                var url = Guid.NewGuid().ToString().Substring(0, 8);

                var exists = this.db.Urls.Any(u => u.ShortUrl == url);
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
