using BestReads.Data.Models;
using BestReads.Services.Shared.Contracts;

namespace BestReads.Services.Contracts
{
    public interface IUserProfileService : IDeletableSharedService<UserProfile>
    {
    }
}
