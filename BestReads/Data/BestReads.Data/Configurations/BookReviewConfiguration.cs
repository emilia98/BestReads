using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BestReads.Data.Configurations
{
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> bookReview)
        {
            bookReview
                .HasOne(br => br.User)
                .WithMany(u => u.BookReviews)
                .HasForeignKey(br => br.UserId);

            bookReview
                .HasOne(br => br.Book)
                .WithMany(b => b.BookReviews)
                .HasForeignKey(br => br.BookId);
        }
    }
}
