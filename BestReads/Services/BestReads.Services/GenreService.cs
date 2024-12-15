using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class GenreService : DeletableSharedService<Genre>, IGenreService
    {
        public GenreService(IDeletableEntityRepository<Genre> repository) : base(repository)
        { }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<Genre> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(Genre? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supoorts GenreOutputModel/GenreInputModel.");
            }

            return model!;
        }

        private T? Convert<T>(Genre entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(GenreInputModel))
            {
                model = new GenreInputModel
                {
                    Title = entity.Title,
                    Tag = entity.Tag,
                    Description = entity.Description
                } as T;
            }
            else if (typeof(T) == typeof(GenreOutputModel))
            {
                model = new GenreOutputModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Tag = entity.Tag,
                    Description = entity.Description,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt
                } as T;
            }

            return model;
        }

        public override Genre CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(GenreInputModel))
            {
                throw new InvalidCastException("This method only supports GenreInputModel.");
            }

            var inputModel = data as GenreInputModel;
            var entity = new Genre
            {
                Title = inputModel!.Title,
                Description = inputModel!.Description,
                Tag = inputModel!.Tag
            };

            return entity;
        }

        public override async Task<Genre?> GetEntityToDelete<T>(int id)
        {
            var genre = await base._repository.GetById(id);
            if (genre == null) return null;
            return genre!.IsDeleted == true ? null : genre;
        }

        public override async Task<Genre?> GetEntityToUndelete<T>(int id)
        {
            var genre = await base._repository.GetById(id);
            if (genre == null) return null;
            return genre!.IsDeleted == true ? genre : null;
        }

        public override Genre UpdateEntity<T>(T data, Genre entity)
        {
            if (typeof(T) != typeof(GenreInputModel))
            {
                throw new InvalidCastException("This method only supports GenreInputModel.");
            }

            var inputModel = data as GenreInputModel;
            entity.Title = inputModel!.Title;
            entity.Description = inputModel!.Description;
            entity.Tag = inputModel!.Tag;

            return entity;
        }
    }
}
