using Dtos;
using Extensions;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;
using ServiceInterfaces;

namespace Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _unitOfWork.UserRepository.SetAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return await _unitOfWork.UserRepository.GetAsync(id);
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {

        string generatedPassword = PasswordGenerator.Generate();
        userDto.Password = generatedPassword;

        _unitOfWork.UserRepository.Insert(userDto);
        await _unitOfWork.SaveChangesAsync();

        await _emailService.SendEmailAsync(userDto.Email, "Your Account Password", $"Your temporary password is: {generatedPassword}");

        return userDto;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        _unitOfWork.UserRepository.Update(userDto);
        await _unitOfWork.SaveChangesAsync();
        return userDto;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null)
        {
            return false;
        }

        _unitOfWork.UserRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MovieDto>> GetFavoriteMoviesAsync(int userId)
    {
        return await _unitOfWork.UserRepository.GetFavoriteMoviesAsync(userId);
    }

    public async Task AddFavoriteMovieAsync(int userId, int movieId)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var movie = await _unitOfWork.MovieRepository.GetAsync(movieId);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found");
        }

        user.FavoriteMovies.Add(movie);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        return await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(userId);
        if (user == null || user.Password != currentPassword)
        {
            return false; // User not found or current password is incorrect
        }

        user.Password = newPassword;
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
    public async Task<UserDto> ValidateUserAsync(string username, string password)
    {
        var user = await _unitOfWork.UserRepository.Set()
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        ArgumentNullException.ThrowIfNull(user,nameof(user));
        return user;
    }

    public async Task<bool> IsFavoriteMovieAsync(int userId, int movieId)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(userId);
        if (user == null) return false;

        return user.FavoriteMovies.Any(m => m.Id == movieId);
    }
}
