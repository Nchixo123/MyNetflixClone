using Dtos;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;

namespace Repositories;

internal sealed class UserRepository : RepositoryBase<UserDto>, IUserRepository
{
    public UserRepository(NetflixDbContext context) : base(context)
    {

    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        ArgumentNullException.ThrowIfNull(email, $"The Email must be provided");

        var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

        ArgumentNullException.ThrowIfNull(user, $"The User with the mention Email: {email} does not exist.");

        return user;
    }

    public async Task<IEnumerable<MovieDto>> GetFavoriteMoviesAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.FavoriteMovies)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user?.FavoriteMovies ?? [];
    }

}
