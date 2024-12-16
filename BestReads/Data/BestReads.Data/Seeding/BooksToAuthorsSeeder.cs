using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
	public class BooksToAuthorsSeeder : ISeeder
	{
		private static ICollection<BookAuthor> booksAuthors = new List<BookAuthor>
		{
			new BookAuthor { BookId = 1, AuthorId = 2 },
			new BookAuthor { BookId = 2, AuthorId = 3 },
			new BookAuthor { BookId = 3, AuthorId = 3 },
			new BookAuthor { BookId = 4, AuthorId = 4 },
			new BookAuthor { BookId = 5, AuthorId = 5 }
		};

		public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
		{
			foreach (var bookAuthor in booksAuthors)
			{
				await SeedBookAuthorAsync(dbContext, bookAuthor);
			}
		}

		private static async Task SeedBookAuthorAsync(ApplicationDbContext dbContext, BookAuthor bookAuthor)
		{
			var exists = await dbContext.BooksAuthors
										.AnyAsync(ba => ba.BookId == bookAuthor.BookId && ba.AuthorId == bookAuthor.AuthorId);
			if (exists) return;

			await dbContext.BooksAuthors.AddAsync(bookAuthor);
			await dbContext.SaveChangesAsync();
		}
	}
}
