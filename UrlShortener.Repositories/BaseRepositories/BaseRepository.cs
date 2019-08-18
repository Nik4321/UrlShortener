using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Data.Models;

namespace UrlShortener.Repositories.BaseRepositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly UrlShortenerDbContext db;

        public BaseRepository(UrlShortenerDbContext db)
        {
            this.db = db;
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.db.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await this.db.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<int> Create(TEntity entity)
        {
            await this.db.Set<TEntity>().AddAsync(entity);
            return await this.db.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            this.db.Set<TEntity>().Update(entity);
            return await this.db.SaveChangesAsync();
        }

        public async Task<int> Delete(TKey id)
        {
            var entity = await this.GetById(id);
            if (entity != null)
            {
                this.db.Set<TEntity>().Remove(entity);
            }

           return await this.db.SaveChangesAsync();
        }
    }
}
