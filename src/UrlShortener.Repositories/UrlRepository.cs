
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Data.Models;
using UrlShortener.Repositories.BaseRepositories;

namespace UrlShortener.Repositories
{
    public class UrlRepository : BaseRepository<Url, int>, IUrlRepository
    {
        public UrlRepository(UrlShortenerDbContext db)
            : base(db)
        {
        }

        public Task<Url> GetByShortUrl(string shortUrl)
        {
            return this.FindOneAsync(x => x.ShortUrl == shortUrl);
        }
    }
}
