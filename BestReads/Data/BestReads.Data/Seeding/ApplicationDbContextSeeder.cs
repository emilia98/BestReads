﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BestReads.Data.Seeding
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>
            {
                new UsersSeeder(),
                new RolesSeeder(),
                new UsersToRolesSeeder(),
                new GenresSeeder(),
                new BooksSeeder(),
                new BooksToGenresSeeder(),
                new BookEditionsSeeder(),
                new AuthorsSeeder(),
                new BooksToAuthorsSeeder(),
                new BookReviewsSeeder(),
                new UserProfilesSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger?.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}