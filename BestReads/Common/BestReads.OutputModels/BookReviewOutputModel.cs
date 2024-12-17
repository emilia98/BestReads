namespace BestReads.OutputModels
{
    public class BookReviewOutputModel : BaseDeletableOutputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }
    }
}
