namespace BestReads.OutputModels
{
    public class BookEditionOutputModel : BaseDeletableOutputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public string CoverType { get; set; }

        public string? CoverImageUrl { get; set; } = null;

        public int Pages { get; set; }

        public DateTime DatePublished { get; set; }

        public int BookId { get; set; }

        public BookOutputModel? Book { get; set; } = null;

        public GenreListOutputModel Genres { get; set; } = new GenreListOutputModel();
    }
}
