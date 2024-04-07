using Microsoft.EntityFrameworkCore;
using server.Data;

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
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _contextSet.ToListAsync(); 
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _contextSet.FindAsync(id);
        }
        public void AddAsync(TEntity entity)
        {
            _contextSet.Add(entity);
            SaveChangesAsync();
        }

        public bool DeleteAsync(int id)
        {
            var entity = _contextSet.Find(id);
            if (entity == null)
            {
                return false;
            }
            _contextSet.Remove(entity);
            SaveChangesAsync();
            return true;
        }

        public void UpdateAsync(TEntity entity)
        {
            _contextSet.Update(entity);
            SaveChangesAsync();
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
