namespace DocManagement.Core.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> listObjcts);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll();
        Task UpdateAsync(TEntity obj);
        void Remove(long id);
    }
}
