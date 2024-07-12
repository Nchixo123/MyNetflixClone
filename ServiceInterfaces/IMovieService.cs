using Dtos;

namespace ServiceInterfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto> GetMovieByIdAsync(int id);
    Task<MovieDto?> GetMovieByTitleAsync(string title);
    Task<MovieDto> CreateMovieAsync(MovieDto movieDto);
    Task<MovieDto> UpdateMovieAsync(MovieDto movieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<IEnumerable<MovieDto>> GetTopRatedMoviesAsync(int count);
    Task<IEnumerable<MovieDto>> SearchMoviesAsync(string keyword);
    Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre);
    Task AddUserRatingAsync(int movieId, int userId, decimal rating);
    Task<IEnumerable<MovieDto>> FilterMoviesAsync(string genre, decimal? minRating);
}
