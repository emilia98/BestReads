using BestReads.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BestReads.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        private static ICollection<(ApplicationUser user, string password)> users = new List<(ApplicationUser user, string password)>
        {
            (new ApplicationUser() { UserName = "admin", Email = "admin@gmail.com" }, "Admin.1234"),
            (new ApplicationUser() { UserName = "editor1", Email = "editor@gmail.com" }, "Editor.1234"),
            (new ApplicationUser() { UserName = "user1", Email = "user1@gmail.com" }, "User.1234"),
            (new ApplicationUser() { UserName = "author1", Email = "author1@gmail.com"}, "Author.1234")
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var user in users)
            {
                await SeedUserAsync(userManager, user.user, user.password);
            }
        }

        private static async Task SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password)
        {
            var entity = await userManager.FindByNameAsync(user.UserName!);

            if (entity == null)
            {
                user.NormalizedUserName = user.UserName!.ToUpper();
                user.NormalizedEmail = user.Email!.ToUpper();
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
