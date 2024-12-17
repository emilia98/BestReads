namespace BestReads.OutputModels
{
    public class GenreOutputModel : BaseDeletableOutputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public int BooksCount { get; set; } = 0;
    }
}
