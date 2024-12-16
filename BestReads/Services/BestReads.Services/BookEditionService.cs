using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class BookEditionService : DeletableSharedService<BookEdition>, IBookEditionService
    {
        public BookEditionService(IDeletableEntityRepository<BookEdition> repository) : base(repository)
        { }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<BookEdition> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(BookEdition? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supports BookEditionOutputModel/BookEditionInputModel.");
            }

            return model!;
        }

        private T? Convert<T>(BookEdition entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(BookEditionInputModel))
            {
                model = new BookEditionInputModel
                {
                    Title = entity.Title,
                    Description = entity.Description,
                    ISBN = entity.ISBN,
                    DatePublished = entity.DatePublished,
                    BookId = entity.BookId,
                    CoverImageUrl = entity.CoverImageUrl,
                    CoverType = entity.CoverType,
                    Pages = entity.Pages,
                } as T;
            }
            else if (typeof(T) == typeof(BookEditionOutputModel))
            {
                model = new BookEditionOutputModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    ISBN = entity.ISBN,
                    DatePublished = entity.DatePublished,
                    BookId = entity.BookId,
                    CoverImageUrl = entity.CoverImageUrl,
                    CoverType = entity.CoverType,
                    Pages = entity.Pages,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt,
                } as T;
            }

            return model;
        }

        public override BookEdition CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(BookEditionInputModel))
            {
                throw new Exception("This method only supports BookEditionInputModel.");
            }

            var inputModel = data as BookEditionInputModel;
            var entity = new BookEdition
            {
                Title = inputModel!.Title,
                Description = inputModel!.Description,
                ISBN = inputModel!.ISBN,
                CoverImageUrl = inputModel!.CoverImageUrl,
                DatePublished = inputModel!.DatePublished,
                Pages = inputModel!.Pages,
                CoverType = inputModel!.CoverType,
                BookId  = inputModel!.BookId,
            };

            return entity;
        }

        public override async Task<BookEdition?> GetEntityToDelete<T>(int id)
        {
            var bookEdition = await base._repository.GetById(id);
            if (bookEdition == null) return null;
            return bookEdition!.IsDeleted == true ? null : bookEdition;
        }

        public override async Task<BookEdition?> GetEntityToUndelete<T>(int id)
        {
            var bookEdition = await base._repository.GetById(id);
            if (bookEdition == null) return null;
            return bookEdition!.IsDeleted == true ? bookEdition : null;
        }

        public override BookEdition UpdateEntity<T>(T data, BookEdition entity)
        {
            if (typeof(T) != typeof(BookEditionInputModel))
            {
                throw new InvalidCastException("This method only supports BookEditionInputModel.");
            }

            var inputModel = data as BookEditionInputModel;
            entity.Title = inputModel!.Title;
            entity.Description = inputModel!.Description;
            entity.ISBN = inputModel!.ISBN;
            entity.DatePublished = inputModel!.DatePublished;
            entity.BookId = inputModel!.BookId;
            entity.CoverImageUrl = inputModel!.CoverImageUrl;
            entity.CoverType = inputModel!.CoverType;

            return entity;
        }
    }
}