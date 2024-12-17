using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class AuthorService : DeletableSharedService<Author>, IAuthorService
    {
        public AuthorService(IDeletableEntityRepository<Author> repository) : base(repository)
        { }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<Author> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(Author? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supports AuthorInputModel/AuthorOutputModel.");
            }

            return model!;
        }

        private T? Convert<T>(Author entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(AuthorInputModel))
            {
                model = new AuthorInputModel
                {
                    Name = entity.Name,
                    Biography = entity.Biography,
                    BornIn = entity.BornIn,
                    DateBorn = entity.DateBorn
                } as T;
            }
            else if (typeof(T) == typeof(AuthorOutputModel))
            {
                model = new AuthorOutputModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Biography = entity.Biography,
                    BornIn = entity.BornIn,
                    DateBorn = entity.DateBorn,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt
                } as T;
            }

            return model;
        }

        public override Author CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(AuthorInputModel))
            {
                throw new InvalidCastException("This method only supports AuthorInputModel.");
            }

            var inputModel = data as AuthorInputModel;
            var entity = new Author
            {
                Name = inputModel!.Name,
                Biography = inputModel!.Biography,
                BornIn = inputModel!.BornIn,
                DateBorn = inputModel!.DateBorn
            };

            return entity;
        }

        public override async Task<Author?> GetEntityToDelete<T>(int id)
        {
            var author = await base._repository.GetById(id);
            if (author == null) return null;
            return author!.IsDeleted == true ? null : author;
        }

        public override async Task<Author?> GetEntityToUndelete<T>(int id)
        {
            var author = await base._repository.GetById(id);
            if (author == null) return null;
            return author!.IsDeleted == true ? author : null;
        }

        public override Author UpdateEntity<T>(T data, Author entity)
        {
            if (typeof(T) != typeof(AuthorInputModel))
            {
                throw new InvalidCastException("This method only supports AuthorInputModel");
            }

            var inputModel = data as AuthorInputModel;
            entity.Name = inputModel!.Name;
            entity.Biography = inputModel!.Biography;
            entity.BornIn = inputModel!.BornIn;
            entity.DateBorn = inputModel!.DateBorn;

            return entity;
        }
    }
}