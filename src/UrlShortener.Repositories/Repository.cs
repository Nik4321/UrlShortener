using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Repositories.Results;

namespace UrlShortener.Repositories
{
    /// <inheritdoc/>
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly UrlShortenerDbContext db;

        /// <summary>
        /// Creates an instance of <see cref="Repository{TEntity, TKey}"/>
        /// </summary>
        /// <param name="db">The DB context</param>
        public Repository(UrlShortenerDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <inheritdoc/>
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.db.Set<TEntity>().AnyAsync(filter);
        }

        /// <inheritdoc/>
        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.db.Set<TEntity>()
                .Where(filter)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> entities = this.db.Set<TEntity>().AsQueryable();
            entities = filter == null ? entities : entities.Where(filter);
            return Task.FromResult(entities);
        }

        /// <inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity source, bool save = true)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var entity = await this.db.Set<TEntity>().AddAsync(source);

            if (save)
            {
                await this.SaveAsync();
            }

            return entity.Entity;
        }

        /// <inheritdoc/>
        public async Task<UpdateResult<TEntity>> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, bool save = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var existingEntity = await this.FindOneAsync(filter);
            if (existingEntity == null)
            {
                return UpdateResult<TEntity>.NotFound;
            }

            this.db.Entry(existingEntity).CurrentValues.SetValues(entity);

            if (save)
            {
                await this.SaveAsync();
            }

            return new UpdateResult<TEntity>(existingEntity);
        }

        /// <inheritdoc/>
        public async Task<RemoveResult> RemoveOneAsync(Expression<Func<TEntity, bool>> filter, bool save = true)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.FindOneAsync(filter);
            if (entity == null)
            {
                return RemoveResult.NotFound;
            }

            this.db.Remove(entity);

            if (save)
            {
                await this.SaveAsync();
            }

            return RemoveResult.Success;
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync()
        {
            return await this.db.SaveChangesAsync();
        }
    }
}
