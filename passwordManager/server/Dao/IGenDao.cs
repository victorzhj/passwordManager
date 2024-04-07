using server.Data;

namespace server.Dao
{
    public interface IGenDao<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void AddAsync(TEntity entity);
        bool DeleteAsync(int id);
        void UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
