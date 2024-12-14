using BestReads.Data.Models;
using BestReads.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BestReads.Data.Seeding
{
    public class UsersToRolesSeeder : ISeeder
    {
        private readonly ICollection<(string userName, string roleName)> usersRoles = new List<(string, string)>
        {
            ("admin", GlobalConstants.AdminRole),
            ("user1", GlobalConstants.UserRole),
            ("editor1", GlobalConstants.EditorRole),
            ("author1", GlobalConstants.AuthorRole)
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var userRole in usersRoles)
            {
                await SeedUserToRolesAsync(dbContext, userManager, roleManager, userRole.userName, userRole.roleName);
            }
        }

        private static async Task SeedUserToRolesAsync(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            string userName,
            string roleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var role = await roleManager.FindByNameAsync(roleName);
            if (user == null || role == null) return;

            var exists = dbContext.UserRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
            if (exists) return;

            var userRole = new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            dbContext.UserRoles.Add(userRole);
            await dbContext.SaveChangesAsync();
        }
    }
}
