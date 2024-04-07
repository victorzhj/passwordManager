using Microsoft.EntityFrameworkCore;
using server.Data;
using System.Linq.Expressions;

namespace server.Dao
{
    public class GenDao<TEntity> : IGenDao<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _contextSet;
        public GenDao(ApplicationDbContext context)
        {
            _context = context;
            _contextSet = _context.Set<TEntity>();
        }
        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _contextSet;
            if (filter != null)
            {
                return await query.Where(filter).ToListAsync();
            }
            return await query.ToListAsync(); 
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _contextSet.FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            _contextSet.Add(entity);
            await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _contextSet.Find(id);
            if (entity == null)
            {
                return false;
            }
            _contextSet.Remove(entity);
            await SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCustomAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _contextSet;
            var entities = await query.Where(filter).ToListAsync();
            if (entities.Count == 0)
            {
                return false;
            }
            _contextSet.RemoveRange(entities);
            await SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _contextSet.Update(entity);
            await SaveChangesAsync();
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
