using BestReads.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BestReads.Helpers;

namespace BestReads.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {
        private static ICollection<ApplicationRole> roles = new List<ApplicationRole>
        {
            new ApplicationRole(GlobalConstants.AdminRole),
            new ApplicationRole(GlobalConstants.UserRole),
            new ApplicationRole(GlobalConstants.EditorRole),
            new ApplicationRole(GlobalConstants.AuthorRole)
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in roles)
            {
                await SeedRoleAsync(roleManager, role);
            }
        }

        public static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, ApplicationRole role)
        {
            var entity = await roleManager.FindByNameAsync(role.Name!);

            if (entity == null)
            {
                role.NormalizedName = role.Name!.ToUpper();
                role.CreatedAt = DateTime.UtcNow;

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
