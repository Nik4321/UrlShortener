using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;

namespace UrlShortener.Repositories
{
    /// <summary>
    /// A repository for persisting <see cref="Url"/> objects.
    /// </summary>
    public interface IUrlRepository : IRepository<Url, int>
    {
        /// <summary>
        /// Finds the first <see cref="Url"/> that matches the <paramref name="shortUrl"/> 
        /// </summary>
        /// <param name="shortUrl">The short url by which to query</param>
        /// <returns>A task for the find operation. The result is a <see name="Url"/> if one exists</returns>
        Task<Url> GetByShortUrl(string shortUrl);
    }
}
