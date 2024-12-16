namespace BestReads.OutputModels
{
    public class BookOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Tag { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<GenreOutputModel> Genres { get; set; } = new List<GenreOutputModel>();
    }
}
