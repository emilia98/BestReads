﻿using BestReads.Data.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace BestReads.Data.Models
{
    public class ApplicationUser : IdentityUser<int>, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Roles = new HashSet<IdentityUserRole<int>>();
            this.Claims = new HashSet<IdentityUserClaim<int>>();
            this.Logins = new HashSet<IdentityUserLogin<int>>();
            this.BookReviews = new HashSet<BookReview>();
        }

        public DateTime CreatedAt { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set; }

        public int? AuthorId { get; set; } = null;

        public Author? Author { get; set; } = null;

        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }

        public virtual ICollection<BookReview> BookReviews { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}