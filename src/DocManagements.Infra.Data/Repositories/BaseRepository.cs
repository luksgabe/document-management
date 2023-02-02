using DocManagement.Core.Interfaces.Repositories;
using DocManagements.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DocManagements.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        protected ApplicationDbContext _context { get; }
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = false;
        }
        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public virtual async Task AddAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> listObjcts)
        {
            await _dbSet.AddRangeAsync(listObjcts);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await Task.Run(() => _dbSet.Update(obj));
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Remove(long id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
