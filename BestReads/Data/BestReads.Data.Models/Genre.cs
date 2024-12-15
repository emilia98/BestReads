using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class Genre : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Tag { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
