using BestReads.Data.Common.Models;
using BestReads.Data.Common.Repositories;
using BestReads.Services.Shared;
using BestReads.Services.Shared.Contracts;

namespace BestReads.Services.Shared
{
    public abstract class DeletableSharedService<TEntity> : SharedService<TEntity>, IDeletableSharedService<TEntity>
        where TEntity : BaseDeletableModel<int>
    {
        private IDeletableEntityRepository<TEntity> Repository => (IDeletableEntityRepository<TEntity>)base._repository;

        public DeletableSharedService(IDeletableEntityRepository<TEntity> repository)
            : base(repository)
        { }

        public IEnumerable<T> GetAll<T>(bool? withDeleted = false, int? count = null) where T : class
        {
            IQueryable<TEntity> query;

            if (withDeleted.HasValue && withDeleted.Value == true)
            {
                query = Repository.AllWithDeleted();
            }
            else
            {
                query = Repository.All();
            }

            query = query.OrderByDescending(x => x.Id);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            if (typeof(T) == typeof(TEntity))
            {
                return query.Select(x => (T)(object)x).ToList();
            }

            return ConvertCollectionToModel<T>(query);
        }

        public async Task<T?> GetById<T>(int id, bool? withDeleted = false) where T : class
        {
            var entity = await Repository.GetById(id);
            if (entity == null) return null;


            // If we include deleted, no matter if the entity is deleted or not, return it.
            if (withDeleted.HasValue && withDeleted.Value == true)
            {
                return (entity as object) as T;
            }

            if (entity.IsDeleted) return null;

            if (typeof(T) == typeof(TEntity))
            {
                return (entity as object) as T;
            }
            return ConvertToModel<T>(entity);
        }

        public async Task Undelete<T>(T data) where T : class, new()
        {
            var entity = GetEntityToUndelete(data);
            Repository.Undelete(entity);
            await Repository.SaveChangesAsync();
        }

        public abstract TEntity GetEntityToUndelete<T>(T data);
    }
}