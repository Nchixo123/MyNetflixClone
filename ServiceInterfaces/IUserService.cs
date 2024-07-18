using Dtos;

namespace ServiceInterfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int id);
    Task<IEnumerable<MovieDto>> GetFavoriteMoviesAsync(int userId);
    Task AddFavoriteMovieAsync(int userId, int movieId);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    Task<UserDto> ValidateUserAsync(string username, string password);
    Task<bool> IsFavoriteMovieAsync(int userId, int movieId);

}
