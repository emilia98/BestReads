using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
    public class GenresSeeder : ISeeder
    {
        private static ICollection<Genre> genres = new List<Genre>
        {
            new Genre { Title = "Биография", Tag = "biography", Description = "Detailed accounts of a person’s life, achievements, and legacy, written by someone else." },
            new Genre { Title = "Автобиография", Tag = "autobiography", Description = "Non-fiction accounts of a person’s life, told from their perspective." },
            new Genre { Title = "Романтика", Tag = "romantic", Description = "Centers on love and relationships, often with emotional conflicts and a satisfying resolution." },
            new Genre { Title = "Приключение", Tag = "adventure", Description = "Action-packed stories featuring daring journeys, exploration, and overcoming physical challenges." },
            new Genre { Title = "Чик лит", Tag = "chick-lit", Description = "Lighthearted, humorous stories often focusing on women navigating career, relationships, and friendships." },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var genre in genres)
            {
                await SeedGenreAsync(dbContext, genre);
            }
        }

        private static async Task SeedGenreAsync(ApplicationDbContext dbContext, Genre genre)
        {
            var exists = await dbContext.Genres.AnyAsync(g => g.Tag == genre.Tag);
            if (exists) return;

            await dbContext.AddAsync(genre);
            await dbContext.SaveChangesAsync();
        }
    }
}
