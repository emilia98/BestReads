using BestReads.Data.Models;
using BestReads.Services.Shared.Contracts;

namespace BestReads.Services.Contracts
{
    public interface IGenreService : IDeletableSharedService<Genre>
    {
        IEnumerable<T> GetAllWithBooks<T>(bool? withDeleted = false, int? count = null)
            where T : class;

	}
}
