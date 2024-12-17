using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class BookReviewService : DeletableSharedService<BookReview>, IBookReviewService
    {
        public BookReviewService(IDeletableEntityRepository<BookReview> repository) : base(repository)
        { }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<BookReview> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(BookReview? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supports BookReviewInputModel/BookReviewOutputModel.");
            }

            return model!;
        }

        private T? Convert<T>(BookReview entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(BookReviewInputModel))
            {
                model = new BookReviewInputModel
                {
                    Title = entity.Title,
                    Description = entity.Description,
                    Rating = entity.Rating
                } as T;
            }
            else if (typeof(T) == typeof(BookReviewOutputModel))
            {
                model = new BookReviewOutputModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Rating = entity.Rating,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt
                } as T;
            }

            return model;
        }

        public override BookReview CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(BookReviewInputModel))
            {
                throw new InvalidCastException("This method only supports BookReviewInputModel.");
            }

            var inputModel = data as BookReviewInputModel;
            var entity = new BookReview
            {
                Title = inputModel!.Title,
                Description = inputModel!.Description,
                Rating = inputModel!.Rating
            };

            return entity;
        }

        public override async Task<BookReview?> GetEntityToDelete<T>(int id)
        {
            var bookReview = await base._repository.GetById(id);
            if (bookReview == null) return null;
            return bookReview!.IsDeleted == true ? null : bookReview;
        }

        public override async Task<BookReview?> GetEntityToUndelete<T>(int id)
        {
            var bookReview = await base._repository.GetById(id);
            if (bookReview == null) return null;
            return bookReview!.IsDeleted == true ? bookReview : null;
        }

        public override BookReview UpdateEntity<T>(T data, BookReview entity)
        {
            if (typeof(T) != typeof(BookReviewInputModel))
            {
                throw new InvalidCastException("This method only supports BookReviewInputModel.");
            }

            var inputModel = data as BookReviewInputModel;
            entity.Title = inputModel!.Title;
            entity.Description = inputModel!.Description;
            entity.Rating = inputModel!.Rating;

            return entity;
        }
    }
}