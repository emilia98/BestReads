using BestReads.Data.Common.Models;

namespace BestReads.Services.Shared.Contracts
{
    public interface IDeletableSharedService<TEntity> : ISharedService<TEntity>
        where TEntity : BaseDeletableModel<int>
    {
        IEnumerable<T> GetAll<T>(bool? withDeleted = false, int? count = null) where T : class;

        Task<T?> GetById<T>(int id, bool? withDeleted = false) where T : class;

        Task Undelete<T>(T data) where T : class, new();

        abstract TEntity GetEntityToUndelete<T>(T data);
    }
}