namespace BestReads.OutputModels
{
    public class BaseDeletableOutputModel : BaseOutputModel
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
