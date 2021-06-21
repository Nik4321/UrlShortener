using System;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Repositories.BaseRepositories;

namespace UrlShortener.Repositories
{
    /// <inheritdoc/>
    public class UrlRepository : BaseRepository<Url, int>, IUrlRepository
    {
        /// <summary>
        /// Creates an instance of <see cref="UrlRepository"/>
        /// </summary>
        /// <param name="db">The DB context</param>
        public UrlRepository(UrlShortenerDbContext db)
            : base(db) { }

        /// <inheritdoc/>
        public Task<Url> GetByShortUrl(string shortUrl)
        {
            if (shortUrl == null)
            {
                throw new ArgumentNullException(nameof(shortUrl));
            }

            return this.FindOneAsync(x => x.ShortUrl == shortUrl);
        }
    }
}
