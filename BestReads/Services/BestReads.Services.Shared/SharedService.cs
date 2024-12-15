using BestReads.Data.Common.Models;
using BestReads.Data.Common.Repositories;
using BestReads.Services.Shared.Contracts;

namespace BestReads.Services.Shared
{
    public abstract class SharedService<TEntity> : ISharedService<TEntity>
        where TEntity : BaseModel<int>
    {
        protected readonly IRepository<TEntity> _repository;

        public SharedService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<int> Create<T>(T data) where T : class, new()
        {
            var entity = CreateEntity(data);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete<T>(T data) where T : class, new()
        {
            var entity = GetEntityToDelete(data);
            if (entity == null) return false;

            try
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<T> GetAll<T>(int? count = null) where T : class
        {
            IQueryable<TEntity> query = _repository.All().OrderByDescending(x => x.Id);

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

        public async Task<T?> GetById<T>(int id) where T : class
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return null;

            if (typeof(T) == typeof(TEntity))
            {
                return (entity as object) as T;
            }

            return ConvertToModel<T>(entity);
        }

        public async Task Update<T>(T data, int id) where T : class, new()
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return;

            var updatedEntity = UpdateEntity(data, entity);
            _repository.Update(updatedEntity);
            await _repository.SaveChangesAsync();
        }

        public abstract IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<TEntity> query) where T : class;

        public abstract T ConvertToModel<T>(TEntity? entity) where T : class;

        public abstract TEntity CreateEntity<T>(T data);

        public abstract TEntity UpdateEntity<T>(T data, TEntity entity);

        public abstract TEntity? GetEntityToDelete<T>(T data);
    }
}