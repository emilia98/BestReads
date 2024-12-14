using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
    public class BooksSeeder : ISeeder
    {
        private static ICollection<Book> books = new List<Book>
        {
            new Book { Title = "Времеубежище", Tag = "vremeubejushte" },
            new Book { Title = "Стив Джобс", Tag = "steve_jobs_walter_isaacson"},
            new Book { Title = "Леонардо да Винчи", Tag="leonardo_da_vinchi_walter_isaacson"},
            new Book { Title = "Хари Потър и Философският камък", Tag = "harry_potter_philosophers_stone" },
            new Book { Title = "Гордост и предразсъдъци", Tag = "pride_prejudice" },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var book in books)
            {
                await SeedBookAsync(dbContext, book);
            }
        }

        private static async Task SeedBookAsync(ApplicationDbContext dbContext, Book book)
        {
            var exists = await dbContext.Books.AnyAsync(b => b.Tag == book.Tag);
            if (exists) return;

            await dbContext.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }
    }
}
