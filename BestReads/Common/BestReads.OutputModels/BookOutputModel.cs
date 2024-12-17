namespace BestReads.OutputModels
{
    public class BookOutputModel : BaseDeletableOutputModel
    {
        public string Title { get; set; }

        public string Tag { get; set; }

        public ICollection<GenreOutputModel> Genres { get; set; } = new List<GenreOutputModel>();
    }
}
