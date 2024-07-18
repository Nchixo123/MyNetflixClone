using Dtos;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceInterfaces;

namespace MyNetflixClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public UserController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserModel userModel)
    {
        string defaultEmail = $"{userModel.Username}@example.com";
        string generatedPassword = PasswordGenerator.Generate();

        var userDto = new UserDto
        {
            Username = userModel.Username,
            ProfilePictureUrl = userModel.ProfilePictureUrl,
            Email = defaultEmail,
            Password = generatedPassword,
            Gender = Gender.Male, // Default value, should be set properly
            IsDelete = false
        };

        var createdUser = await _userService.CreateUserAsync(userDto);

        await _emailService.SendEmailAsync(defaultEmail, "Your Account Password", $"Your temporary password is: {generatedPassword}");

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserModel userModel)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
        {
            return NotFound();
        }

        userDto.Username = userModel.Username;
        userDto.ProfilePictureUrl = userModel.ProfilePictureUrl;

        var updatedUser = await _userService.UpdateUserAsync(userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}/favorites")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetFavoriteMovies(int id)
    {
        var movies = await _userService.GetFavoriteMoviesAsync(id);
        return Ok(movies);
    }

    [HttpPost("{id}/favorites/{movieId}")]
    public async Task<IActionResult> AddFavoriteMovie(int id, int movieId)
    {
        await _userService.AddFavoriteMovieAsync(id, movieId);
        return NoContent();
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(int id, string currentPassword, string newPassword)
    {
        var success = await _userService.ChangePasswordAsync(id, currentPassword, newPassword);
        if (!success)
        {
            return BadRequest("Current password is incorrect or user not found.");
        }
        return NoContent();
    }

    [HttpGet("{userId}/favorite/{movieId}")]
    public async Task<IActionResult> IsFavoriteMovie(int userId, int movieId)
    {
        var isFavorite = await _userService.IsFavoriteMovieAsync(userId, movieId);
        return Ok(new { IsFavorite = isFavorite });
    }
}
