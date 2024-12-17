using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class BookReview : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Rating { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Book Book { get; set; }
    }
}