using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
	public class BookEdition : BaseDeletableModel<int>
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string ISBN { get; set; }

		public string? CoverImageUrl { get; set; } = null;

		public string CoverType { get; set; }

		public int Pages { get; set; }

		public DateTime DatePublished { get; set; }

		public int BookId { get; set; }

		public virtual Book Book { get; set; }

		// public Language Language { get; set; }
	}
}
