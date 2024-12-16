using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class BookService : DeletableSharedService<Book>, IBookService
    {
        public BookService(IDeletableEntityRepository<Book> repository) : base(repository)
        {  }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<Book> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(Book? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supports BookOutputModel/BookInputModel.");
            }

            return model!;
        }

        private T? Convert<T>(Book entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(BookInputModel))
            {
                model = new BookInputModel
                {
                    Title = entity.Title,
                    Tag = entity.Tag
                } as T;
            }
            else if (typeof(T) == typeof(BookOutputModel))
            {
                model = new BookOutputModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Tag = entity.Tag,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt
                } as T;
            }

            return model;
        }

        public override Book CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(BookInputModel))
            {
                throw new InvalidCastException("This method only supports BookInputModel.");
            }

            var inputModel = data as BookInputModel;
            var entity = new Book
            {
                Title = inputModel!.Title,
                Tag = inputModel!.Tag
            };

            return entity;
        }

        public override async Task<Book?> GetEntityToDelete<T>(int id)
        {
            var book = await base._repository.GetById(id);
            if (book == null) return null;
            return book!.IsDeleted == true ? null : book;
        }

        public override async Task<Book?> GetEntityToUndelete<T>(int id)
        {
            var book = await base._repository.GetById(id);
            if (book == null) return null;
            return book!.IsDeleted == true ? book : null;
        }

        public override Book UpdateEntity<T>(T data, Book entity)
        {
            if (typeof(T) != typeof(BookInputModel))
            {
                throw new InvalidCastException("This method only supports BookInputModel.");
            }

            var inputModel = data as BookInputModel;
            entity.Title = inputModel!.Title;
            entity.Tag = inputModel!.Tag;

            return entity;
        }
    }
}