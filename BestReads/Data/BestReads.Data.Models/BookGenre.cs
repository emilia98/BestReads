using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class BookGenre : JoinModel
    {
        public int BookId { get; set; }

        public int GenreId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
