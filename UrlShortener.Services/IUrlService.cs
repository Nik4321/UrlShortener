using UrlShortener.Data.Models;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Url ShortenUrl(string longUrl, long? expireDate = null);

        Url GetUrlByShortUrl(string shortUrl);

        bool HasUrlExpired(Url url);
    }
}
