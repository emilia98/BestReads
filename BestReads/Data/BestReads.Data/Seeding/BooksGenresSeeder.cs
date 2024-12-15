using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
    public class BooksGenresSeeder : ISeeder
    {
        private static ICollection<BookGenre> booksGenres = new List<BookGenre>
        {
            new BookGenre { BookId = 2, GenreId = 1 },
            new BookGenre { BookId = 2, GenreId = 2 },
            new BookGenre { BookId = 3, GenreId = 1 },
            new BookGenre { BookId = 4, GenreId = 4 },
            new BookGenre { BookId = 5, GenreId = 3 }
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var bookGenre in booksGenres)
            {
                await SeedBookGenreAsync(dbContext, bookGenre);
            }
        }

        private static async Task SeedBookGenreAsync(ApplicationDbContext dbContext, BookGenre bookGenre)
        {
            var bookExists = await dbContext.Books.AnyAsync(b => b.Id == bookGenre.BookId);
            var genreExists = await dbContext.Genres.AnyAsync(g => g.Id == bookGenre.GenreId);
            if (!bookExists || !genreExists) return;

            var bookGenreExists = await dbContext.BooksGenres
                .AnyAsync(bg => bg.BookId == bookGenre.BookId && bg.GenreId == bookGenre.GenreId);
            if (bookGenreExists) return;

            await dbContext.BooksGenres.AddAsync(bookGenre);
            await dbContext.SaveChangesAsync();
        }
    }
}
