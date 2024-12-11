using BestReads.Data.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace BestReads.Data.Models
{
    public class ApplicationRole : IdentityRole<int>, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole() : this(null)
        { }

        public ApplicationRole(string name) : base(name)
        { }

        public DateTime CreatedAt { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set ; }
    }
}