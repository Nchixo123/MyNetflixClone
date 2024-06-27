using Dtos;

namespace ServiceInterfaces;

public interface IUserRatingService
{
    Task<UserRatingsDto> RateMovieAsync(UserRatingsDto userRatingDto);
    Task<UserRatingsDto> UpdateRatingAsync(UserRatingsDto userRatingDto);
    Task<bool> RemoveRatingAsync(int userId, int movieId);
}
