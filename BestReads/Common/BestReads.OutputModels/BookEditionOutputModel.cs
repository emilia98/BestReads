namespace BestReads.OutputModels
{
    public class BookEditionOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public string CoverType { get; set; }

        public string? CoverImageUrl { get; set; } = null;

        public int Pages { get; set; }

        public DateTime DatePublished { get; set; }

        public int BookId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; } = null;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public BookOutputModel? Book { get; set; } = null;

        public GenreListOutputModel Genres { get; set; } = new GenreListOutputModel();
    }
}
