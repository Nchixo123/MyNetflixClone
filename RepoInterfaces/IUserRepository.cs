using Dtos;
namespace RepoInterfaces;
public interface IUserRepository : IRepositoryBase<UserDto>
{
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<IEnumerable<MovieDto>> GetFavoriteMoviesAsync(int userId);
}
