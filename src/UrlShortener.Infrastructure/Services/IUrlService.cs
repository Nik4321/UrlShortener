using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;

namespace UrlShortener.Infrastructure.Services
{
    /// <summary>
    /// A service that will handle <see cref="Url"/> operations.
    /// </summary>
    public interface IUrlService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="longUrl"></param>
        /// <param name="expireDate"></param>
        /// <returns></returns>
        Task<Url> ShortenUrl(string longUrl, long? expireDate = null);

        /// <summary>
        /// Retrieves a <see cref="Url"/> from the repository.
        /// </summary>
        /// <param name="shortUrl">The short url by which to find the <see cref="Url"/></param>
        /// <returns>A task of <see cref="Url" /> if one exists</returns>
        Task<Url> GetUrl(string shortUrl);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool HasUrlExpired(Url url);
    }
}
