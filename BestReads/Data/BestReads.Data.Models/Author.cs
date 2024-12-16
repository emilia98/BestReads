using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
	public class Author : BaseDeletableModel<int>
	{
		public string Name { get; set; }

		public string Biography { get; set; }

		public string BornIn { get; set; }

		public DateTime? DateBorn { get; set; } = null;

		public int? UserId { get; set; } = null;

		public ApplicationUser? User { get; set; } = null;

		// public Nationality Nationality { get; set; }

		public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
	}
}
