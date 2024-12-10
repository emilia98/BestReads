namespace BestReads.Data.Common.Models
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get ; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}
