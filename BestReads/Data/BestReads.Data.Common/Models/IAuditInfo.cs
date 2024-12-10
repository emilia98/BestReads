namespace BestReads.Data.Common.Models
{
    public interface IAuditInfo
    {
        DateTime CreatedAt { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}
