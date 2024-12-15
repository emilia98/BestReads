using BestReads.Data.Common.Models;

namespace BestReads.Services.Shared.Contracts
{
    public interface ISharedService<TEntity> where TEntity : BaseModel<int>
    {
        Task<int> Create<T>(T data) where T : class, new();

        IEnumerable<T> GetAll<T>(int? count = null) where T : class;

        Task<T?> GetById<T>(int id) where T : class;

        Task Update<T>(T data, int entityId) where T : class, new();

        Task<bool> Delete<T>(int id) where T : class, new();

        abstract IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<TEntity> query) where T : class;

        abstract T ConvertToModel<T>(TEntity? entity) where T : class;

        abstract TEntity CreateEntity<T>(T data);

        abstract TEntity UpdateEntity<T>(T data, TEntity entity);

        abstract Task<TEntity?> GetEntityToDelete<T>(T data);
    }
}