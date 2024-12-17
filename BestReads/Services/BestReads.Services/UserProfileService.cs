using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.InputModels;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using BestReads.Services.Shared;

namespace BestReads.Services
{
    public class UserProfileService : DeletableSharedService<UserProfile>, IUserProfileService
    {
        public UserProfileService(IDeletableEntityRepository<UserProfile> repository) : base(repository)
        { }

        public override IEnumerable<T> ConvertCollectionToModel<T>(IQueryable<UserProfile> query)
        {
            return query.ToList().Select(e => ConvertToModel<T>(e));
        }

        public override T ConvertToModel<T>(UserProfile? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("To convert to model, the entity shouldn't be null.");
            }

            var model = Convert<T>(entity);
            if (model == null)
            {
                throw new InvalidCastException("This method only supports UserProfileInputModel/UserProfileOutputModel.");
            }

            return model!;
        }

        private T? Convert<T>(UserProfile entity) where T : class
        {
            T? model = null;

            if (typeof(T) == typeof(UserProfileInputModel))
            {
                model = new UserProfileInputModel
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    CountryFrom = entity.CountryFrom,
                    DateBorn = entity.DateBorn
                } as T;
            }
            else if (typeof(T) == typeof(UserProfileOutputModel))
            {
                model = new UserProfileOutputModel
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    CountryFrom = entity.CountryFrom,
                    DateBorn = entity.DateBorn,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    IsDeleted = entity.IsDeleted,
                    DeletedAt = entity.DeletedAt
                } as T;
            }

            return model;
        }

        public override UserProfile CreateEntity<T>(T data)
        {
            if (typeof(T) != typeof(UserProfileInputModel))
            {
                throw new InvalidCastException("This method only supports UserProfileInputModel.");
            }

            var inputModel = data as UserProfileInputModel;
            var entity = new UserProfile
            {
                FirstName = inputModel!.FirstName,
                LastName = inputModel!.LastName,
                CountryFrom = inputModel!.CountryFrom,
                DateBorn = inputModel!.DateBorn
            };

            return entity;
        }

        public override async Task<UserProfile?> GetEntityToDelete<T>(int id)
        {
            var userProfile = await base._repository.GetById(id);
            if (userProfile == null) return null;
            return userProfile!.IsDeleted == true ? null : userProfile;
        }

        public override async Task<UserProfile?> GetEntityToUndelete<T>(int id)
        {
            var userProfile = await base._repository.GetById(id);
            if (userProfile == null) return null;
            return userProfile!.IsDeleted == true ? userProfile : null;
        }

        public override UserProfile UpdateEntity<T>(T data, UserProfile entity)
        {
            if (typeof(T) != typeof(UserProfileInputModel))
            {
                throw new InvalidCastException("This method only supports UserProfileInputModel.");
            }

            var inputModel = data as UserProfileInputModel;
            entity.FirstName = inputModel!.FirstName;
            entity.LastName = inputModel!.LastName;
            entity.CountryFrom = inputModel!.CountryFrom;
            entity.DateBorn = inputModel!.DateBorn;
            return entity;
        }
    }
}