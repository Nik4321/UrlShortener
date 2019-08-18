using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repositories.BaseRepositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TKey id);

        Task<int> Create(TEntity entity);

        Task<int> Update(TEntity entity);

        Task<int> Delete(TKey id);
    }
}
