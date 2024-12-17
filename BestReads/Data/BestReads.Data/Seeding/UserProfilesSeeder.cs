using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
    public class UserProfilesSeeder : ISeeder
    {
        private static ICollection<UserProfile> userProfiles = new List<UserProfile>
        {
            new UserProfile { FirstName = "Admin", LastName = "Adminov", DateBorn = new DateTime(1998,7,8), CountryFrom = "Bulgaria", UserId = 1 },
            new UserProfile { FirstName = "Editor", LastName = "Editor", DateBorn = new DateTime(1985,10,11), CountryFrom = "USA", UserId = 2 },
            new UserProfile { FirstName = "Andrei", LastName = "Popescu", DateBorn = new DateTime(1983,12,12), CountryFrom = "Romania", UserId = 3 },
            new UserProfile { FirstName = "Иван", LastName = "Иванов", DateBorn = new DateTime(1971,5,30), CountryFrom = "Bulgaria", UserId = 4 },
            new UserProfile { FirstName = "Nikos", LastName = "Papadopoulos", DateBorn = new DateTime(1980,4,5), CountryFrom = "Greece", UserId = 5 },
            new UserProfile { FirstName = "Maria", LastName = "Petrova", DateBorn = new DateTime(1998,1,1), CountryFrom = "Bulgaria", UserId = 6 },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var userProfile in userProfiles)
            {
                await SeedUserProfileAsync(dbContext, userProfile);
            }
        }

        private static async Task SeedUserProfileAsync(ApplicationDbContext dbContext, UserProfile userProfile)
        {
            var exists = await dbContext.UserProfiles.AnyAsync(up => up.UserId == userProfile.UserId);
            if (exists) return;

            await dbContext.UserProfiles.AddAsync(userProfile);
            await dbContext.SaveChangesAsync();
        }
    }
}
