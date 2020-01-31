using System.Threading.Tasks;
using UrlShortener.Data.Models;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<Url> ShortenUrl(string longUrl, long? expireDate = null);

        Task<Url> GetUrlByShortUrl(string shortUrl);

        bool HasUrlExpired(Url url);
    }
}
