using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BestReads.Data.Configurations
{
	public class AuthorUserConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> author)
		{
			author
				.HasOne(a => a.User)
				.WithOne(u => u.Author)
				.HasForeignKey<ApplicationUser>(u => u.AuthorId);
		}
	}
}
