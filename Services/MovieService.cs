using Dtos;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;
using ServiceInterfaces;

namespace Services;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRatingService _userRatingService;

    public MovieService(IUnitOfWork unitOfWork, IUserRatingService userRatingService)
    {
        _unitOfWork = unitOfWork;
        _userRatingService = userRatingService;
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        return await _unitOfWork.MovieRepository.SetAsync();
    }

    public async Task<MovieDto> GetMovieByIdAsync(int id)
    {
        return await _unitOfWork.MovieRepository.GetAsync(id);
    }

    public async Task<MovieDto> CreateMovieAsync(MovieDto movieDto)
    {
        ArgumentNullException.ThrowIfNull(movieDto, nameof(movieDto));
        _unitOfWork.MovieRepository.Insert(movieDto);
        await _unitOfWork.SaveChangesAsync();
        return movieDto;
    }

    public async Task<MovieDto> UpdateMovieAsync(MovieDto movieDto)
    {
        _unitOfWork.MovieRepository.Update(movieDto);
        await _unitOfWork.SaveChangesAsync();
        return movieDto;
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var movie = await _unitOfWork.MovieRepository.GetAsync(id);
        if (movie == null)
        {
            return false;
        }

        _unitOfWork.MovieRepository.Delete(movie);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MovieDto>> GetTopRatedMoviesAsync(int count)
    {
        return await _unitOfWork.MovieRepository.GetTopRatedMoviesAsync(count);
    }

    public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string keyword)
    {
        return await _unitOfWork.MovieRepository.SearchMoviesAsync(keyword);
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre)
    {
        return await _unitOfWork.MovieRepository.GetMoviesByGenreAsync(genre);
    }

    public async Task AddUserRatingAsync(int movieId, int userId, decimal rating)
    {
        var userRatingDto = new UserRatingsDto
        {
            MovieId = movieId,
            UserId = userId,
            Rating = rating
        };

        await _userRatingService.RateMovieAsync(userRatingDto);

        var movie = await _unitOfWork.MovieRepository.GetAsync(movieId);
        var ratings = await _unitOfWork.UserRatingRepository.Set()
                                    .Where(ur => ur.MovieId == movieId)
                                    .ToListAsync();

        movie.AverageRating = ratings.Average(r => r.Rating);
        _unitOfWork.MovieRepository.Update(movie);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<MovieDto?> GetMovieByTitleAsync(string title)
    {
        return await _unitOfWork.MovieRepository.Set(m => m.Title == title).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MovieDto>> FilterMoviesAsync(string genre, decimal? minRating)
    {
        var query = _unitOfWork.MovieRepository.Set();

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => m.Genre == genre);
        }

        if (minRating.HasValue)
        {
            query = query.Where(m => m.AverageRating >= minRating);
        }

        return await query.ToListAsync();
    }

}
