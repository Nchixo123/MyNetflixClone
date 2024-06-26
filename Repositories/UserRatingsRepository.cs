using Dtos;
using RepoInterfaces;

namespace Repositories;

internal class UserRatingsRepository : RepositoryBase<UserRatingsDto>, IUserRatingsRepository
{
    public UserRatingsRepository(NetflixDbContext context) : base(context)
    {

    }
}
