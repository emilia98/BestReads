using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BestReads.Data.Configurations
{
	public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
	{
		public void Configure(EntityTypeBuilder<BookAuthor> bookAuthor)
		{
			// Create a composite key
			bookAuthor
				.HasKey(ba => new { ba.BookId, ba.AuthorId });

			bookAuthor
				.HasOne(ba => ba.Book)
				.WithMany(b => b.BookAuthors)
				.HasForeignKey(ba => ba.BookId);

			bookAuthor
				.HasOne(ba => ba.Author)
				.WithMany(a => a.BookAuthors)
				.HasForeignKey(ba => ba.AuthorId);
		}
	}
}
