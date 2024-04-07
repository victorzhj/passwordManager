using server.Data;
using System.Linq.Expressions;

namespace server.Dao
{
    public interface IGenDao<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetByIdAsync(int id);

        void AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteCustomAsync(Expression<Func<TEntity, bool>> filter);
        void UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
