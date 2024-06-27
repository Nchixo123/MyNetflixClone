using Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;
using ServiceInterfaces;

namespace MyNetflixClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRatingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRatingService _userRatingService;

    public UserRatingController(IUnitOfWork unitOfWork, IUserRatingService userRatingService)
    {
        _unitOfWork = unitOfWork;
        _userRatingService = userRatingService;
    }

    [HttpPost]
    public async Task<ActionResult<UserRatingsDto>> RateMovie(UserRatingsDto userRatingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdRating = await _userRatingService.RateMovieAsync(userRatingDto);
        return CreatedAtAction(nameof(RateMovie), new { id = createdRating.Id }, createdRating);
    }

    [HttpPut("{userId}/{movieId}")]
    public async Task<IActionResult> UpdateRating(int userId, int movieId, [FromBody] decimal newRating)
    {
        var userRatingDto = await _unitOfWork.UserRatingRepository.Set()
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.MovieId == movieId);

        if (userRatingDto == null)
        {
            return NotFound();
        }

        userRatingDto.Rating = newRating;

        await _userRatingService.UpdateRatingAsync(userRatingDto);
        return NoContent();
    }

    [HttpDelete("{userId}/{movieId}")]
    public async Task<IActionResult> RemoveRating(int userId, int movieId)
    {
        var success = await _userRatingService.RemoveRatingAsync(userId, movieId);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
