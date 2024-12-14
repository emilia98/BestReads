using BestReads.Data.Common.Models;

namespace BestReads.Data.Models
{
    public class Book : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Tag { get; set; }
    }
}
