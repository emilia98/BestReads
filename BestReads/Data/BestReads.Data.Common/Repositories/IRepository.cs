using BestReads.Data.Common.Models;

namespace BestReads.Data.Common.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : BaseModel<int>
    {
        IQueryable<TEntity> All();

        Task<TEntity?> GetById(int id);

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}