namespace BestReads.InputModels
{
    public class AuthorInputModel
    {
        public string Name { get; set; }

        public string Biography { get; set; }

        public string BornIn { get; set; }

        public DateTime? DateBorn { get; set; } = null;
    }
}
