using server.Data;
using System.Linq.Expressions;

namespace server.Dao
{
    /// <summary>
    /// Represents a generic data access object interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IGenDao<TEntity>
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <param name="filter">An optional filter expression to apply.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved entity.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the entity was deleted successfully.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Deletes entities based on a custom filter expression asynchronously.
        /// </summary>
        /// <param name="filter">The custom filter expression to apply.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether any entities were deleted.</returns>
        Task<bool> DeleteCustomAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Saves changes made to the data source asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveChangesAsync();
    }
}
