using Dtos;
using RepoInterfaces;
using ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserRatingService : IUserRatingService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRatingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRatingsDto> RateMovieAsync(UserRatingsDto userRatingDto)
    {
        _unitOfWork.UserRatingRepository.Insert(userRatingDto);
        await _unitOfWork.SaveChangesAsync();

        await UpdateMovieAverageRating(userRatingDto.MovieId);

        return userRatingDto;
    }

    public async Task<UserRatingsDto> UpdateRatingAsync(UserRatingsDto userRatingDto)
    {
        _unitOfWork.UserRatingRepository.Update(userRatingDto);
        await _unitOfWork.SaveChangesAsync();

        await UpdateMovieAverageRating(userRatingDto.MovieId);

        return userRatingDto;
    }

    public async Task<bool> RemoveRatingAsync(int userId, int movieId)
    {
        var userRating = await _unitOfWork.UserRatingRepository.Set()
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.MovieId == movieId);

        if (userRating == null)
        {
            return false;
        }

        _unitOfWork.UserRatingRepository.Delete(userRating);
        await _unitOfWork.SaveChangesAsync();

        await UpdateMovieAverageRating(movieId);

        return true;
    }

    private async Task UpdateMovieAverageRating(int movieId)
    {
        var movie = await _unitOfWork.MovieRepository.GetAsync(movieId);
        var ratings = await _unitOfWork.UserRatingRepository.Set()
                            .Where(ur => ur.MovieId == movieId)
                            .ToListAsync();

        movie.AverageRating = ratings.Count != 0 ? ratings.Average(r => r.Rating) : null;
        _unitOfWork.MovieRepository.Update(movie);
        await _unitOfWork.SaveChangesAsync();
    }
}
