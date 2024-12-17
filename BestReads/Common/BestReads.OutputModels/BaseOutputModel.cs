namespace BestReads.OutputModels
{
    public class BaseOutputModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
