using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BestReads.Data.Configurations
{
	public class BookEditionConfiguration : IEntityTypeConfiguration<BookEdition>
	{
		public void Configure(EntityTypeBuilder<BookEdition> bookEdition)
		{
			bookEdition
				.HasOne(be => be.Book)
				.WithMany(b => b.BookEditions)
				.HasForeignKey(be => be.BookId);
		}
	}
}