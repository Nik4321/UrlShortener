using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repositories.BaseRepositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TKey id);

        Task Create(TEntity entity);

        void Update(TEntity entity);

        Task Delete(TKey id);

        Task SaveChangesAsync();
    }
}
