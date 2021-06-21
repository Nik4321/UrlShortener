using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Repositories.Results;

namespace UrlShortener.Repositories
{
    /// <summary>
    /// A repository for performing common operations on the database
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The entity primary key type</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Checks if a <typeparamref name="TEntity"/> is found using the <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">The filter for finding a <typeparamref name="TEntity"/></param>
        /// <returns>A task for the existence check operation. The result is true if the <typeparamref name="TEntity"/> exists, false if not</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Finds the first <typeparamref name="TEntity"/> in the repository that meets the <paramref name="filter"/> criteria
        /// </summary>
        /// <param name="filter">The filter to be applied to the query</param>
        /// <returns>A task for the find operation. The result is a <typeparamref name="TEntity"/> if one exists</returns>
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets a list of <see cref="IQueryable{TEntity}"/> in the repository that meet the (optional) filter criteria
        /// </summary>
        /// <param name="filter">The filter to be applied to the query</param>
        /// <returns>A task for the get list operation. The result is a list of <see cref="IQueryable{TEntity}"/></returns>
        Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="source">The entity to add to the repository</param>
        /// <param name="save">Whether or not a SaveChanges should be performed</param>
        /// <returns>A task for the add operation. The result is the added entity</returns>
        Task<TEntity> AddAsync(TEntity source, bool save = true);

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="TEntity"/> containing the new values</param>
        /// <param name="filter">The filter for finding the existing <typeparamref name="TEntity"/></param>
        /// <param name="save">Whether or not a SaveChanges should be performed</param>
        /// <returns>The updated entity</returns>
        Task<UpdateResult<TEntity>> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, bool save = true);

        /// <summary>
        /// Removes an entity from the repository
        /// </summary>
        /// <param name="filter">The filter for finding a <typeparamref name="TEntity"/></param>
        /// <param name="save">Whether or not a SaveChanges should be performed</param>
        /// <returns>A task for the remove operation. The result is a task of <see cref="RemoveResult"/></returns>
        Task<RemoveResult> RemoveOneAsync(Expression<Func<TEntity, bool>> filter, bool save = true);

        /// <summary>
        /// Save and audit changes
        /// </summary>
        /// <returns>A task for the save operation. The result is the number of records affected</returns>
        Task<int> SaveAsync();
    }
}
