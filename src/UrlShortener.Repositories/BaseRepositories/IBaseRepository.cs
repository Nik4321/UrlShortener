using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShortener.Repositories.Results;

namespace UrlShortener.Repositories.BaseRepositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);
        
        Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> AddAsync(TEntity source, bool save = true);

        Task<UpdateResult<TEntity>> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, bool save = true);

        Task<RemoveResult> RemoveOneAsync(Expression<Func<TEntity, bool>> filter, bool save = true);
    }
}
