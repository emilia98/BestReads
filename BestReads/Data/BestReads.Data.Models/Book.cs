using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class Book : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Tag { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

        public virtual ICollection<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
