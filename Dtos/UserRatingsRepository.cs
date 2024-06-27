using Dtos;
using RepoInterfaces;

namespace Repositories;

internal sealed class UserRatingsRepository : RepositoryBase<UserRatingsDto>, IUserRatingsRepository
{
    public UserRatingsRepository(NetflixDbContext context) : base(context)
    {

    }
}
