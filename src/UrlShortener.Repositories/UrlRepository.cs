using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Url> GetByShortUrl(string shortUrl)
        {
            return await this.GetAll().FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
        }
    }
}
