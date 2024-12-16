using BestReads.Data.Models;

namespace BestReads.Data.Seeding
{
	public class BookEditionsSeeder : ISeeder
	{
		private static ICollection<BookEdition> bookEditions = new List<BookEdition>
		{
			new BookEdition { Title = "Времеубежище", ISBN = "9786191866052", Description = "", Pages = 372, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2020,04,29), BookId = 1, },
			new BookEdition { Title = "Time Shelter", ISBN = "9781474623070", Description = "", Pages = 304, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2023,03,30), BookId = 1, },
			new BookEdition { Title = "Стив Джобс", ISBN = "9789546859990", Description = "", Pages = 672, CoverType = "hardcover", CoverImageUrl = null, DatePublished = new DateTime(2011,1,1), BookId = 2, },
			new BookEdition { Title = "Steve Jobs", ISBN = "9781451648539 ", Description = "", Pages = 632, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2011,1,1), BookId = 2, },
			new BookEdition { Title = "Леонардо да Винчи", ISBN = "9786191514489", Description = "", Pages = 552, CoverType = "hardcover", CoverImageUrl = null, DatePublished = new DateTime(2015,1,1), BookId = 3, },
			new BookEdition { Title = "Leonardo Da Vinci", ISBN = "9781501139154", Description = "", Pages = 624, CoverType = "hardcover", CoverImageUrl = null, DatePublished = new DateTime(0217,10,17), BookId = 3, },
			new BookEdition { Title = "Хари Потър и философският камък", ISBN = "9789544464684", Description = "", Pages = 264, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2007,1,1), BookId = 4, },
			new BookEdition { Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532699", Description = "", Pages = 223, CoverType = "hardcover", CoverImageUrl = null, DatePublished = new DateTime(1997,06,26), BookId = 4, },
			new BookEdition { Title = "Гордост и предразсъдъци", ISBN = "9789543190843", Description = "", Pages = 498, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2010,1,1), BookId = 5, },
			new BookEdition { Title = "Pride and Prejudice", ISBN = "9780141439518", Description = "", Pages = 448, CoverType = "paperback", CoverImageUrl = null, DatePublished = new DateTime(2003,01,30), BookId = 5, },
		};

		public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
		{
			if (dbContext.BookEditions.Count() > 0) return;

			foreach (var bookEdition in bookEditions)
			{
				await SeedBookEditionAsync(dbContext, bookEdition);
			}
		}

		private static async Task SeedBookEditionAsync(ApplicationDbContext dbContext, BookEdition bookEdition)
		{
			await dbContext.BookEditions.AddAsync(bookEdition);
			await dbContext.SaveChangesAsync();
		}
	}
}
