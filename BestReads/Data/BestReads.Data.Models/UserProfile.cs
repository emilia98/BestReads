using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class UserProfile : BaseDeletableModel<int>
    {
        public string? FirstName { get; set; } = null;

        public string? LastName { get; set; } = null;

        public string? CountryFrom { get; set; } = null;

        public DateTime? DateBorn { get; set; } = null;

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
