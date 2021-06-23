using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;

namespace UrlShortener.Data.Repositories
{
    /// <inheritdoc/>
    public class UrlRepository : Repository<Url, int>, IUrlRepository
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
