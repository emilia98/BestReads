using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
	public class BookAuthor : JoinModel
	{
		public int BookId { get; set; }

		public virtual Book Book { get; set; }

		public int AuthorId { get; set; }

		public virtual Author Author { get; set; }
	}
}
