namespace BestReads.OutputModels
{
    public class AuthorOutputModel : BaseDeletableOutputModel
    {
        public string Name { get; set; }

        public string Biography { get; set; }

        public string BornIn { get; set; }

        public DateTime? DateBorn { get; set; } = null;

        public ICollection<BookEditionOutputModel> Books = new List<BookEditionOutputModel>();
    }
}
