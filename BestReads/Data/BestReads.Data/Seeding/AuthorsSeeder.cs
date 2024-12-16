using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestReads.Data.Seeding
{
	public class AuthorsSeeder : ISeeder
	{
		private static ICollection<Author> authors = new List<Author>
		{
			new Author { Name = "Unknown Name", Biography = "", BornIn = "Unknown", DateBorn = new DateTime(1990, 1, 1), UserId = 4 },
			new Author { Name = "Георги Господинов", Biography = "", BornIn = "Ямбол, България", DateBorn = new DateTime(1968,1,7), UserId = null },
			new Author { Name = "Walter Isaacson", Biography = "", BornIn = "New Orleans, United States", DateBorn = new DateTime(1952,5,20), UserId = null },
			new Author { Name = "J. K. Rowling", Biography = "Yate, Gloucestershire, United Kingdom", BornIn = "", DateBorn = new DateTime(1965,7,31), UserId = null },
			new Author { Name = "Jane Austen", Biography = "Steventon, Hampshire, United Kingdom", BornIn = "", DateBorn = new DateTime(1775,12,16), UserId = null },
		};

		public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
		{
			foreach (var author in authors)
			{
				await SeedAuthorAsync(dbContext, author);
			}
		}

		private static async Task SeedAuthorAsync(ApplicationDbContext dbContext, Author author)
		{
			var exists = await dbContext.Authors.AnyAsync(a => a.Name == author.Name);
			if (exists) return;

			await dbContext.Authors.AddAsync(author);
			await dbContext.SaveChangesAsync();
		}
	}
}
