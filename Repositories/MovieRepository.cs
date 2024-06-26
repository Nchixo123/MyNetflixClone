using Dtos;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;

namespace Repositories;

internal class MovieRepository : RepositoryBase<MovieDto>, IMovieRepository
{
    public MovieRepository(NetflixDbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<MovieDto>> GetTopRatedMoviesAsync(int count)
    {
        return await _dbSet.OrderByDescending(m => m.AverageRating).Take(count).ToListAsync();
    }

    public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string keyword)
    {
        return await _dbSet
            .Where(m => m.Title.Contains(keyword) || m.Description.Contains(keyword))
            .ToListAsync();
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre)
    {
        return await _dbSet.Where(m => m.Genre == genre).ToListAsync();
    }

    public async Task AddUserRatingAsync(int movieId, int userId, decimal rating)
    {
        var movie = await _dbSet.Include(m => m.UserRatings).FirstOrDefaultAsync(m => m.Id == movieId);
        ArgumentNullException.ThrowIfNull(movie, $"The movie with the given Id: {movieId} does not exist");

        var userRating = new UserRatingsDto
        {
            MovieId = movieId,
            UserId = userId,
            Rating = rating
        };

        ArgumentNullException.ThrowIfNull(userRating, "The movie hasnt been reviewed");

        movie.UserRatings.Add(userRating);
        movie.AverageRating = movie.UserRatings.Average(ur => ur.Rating);

        _context.UserRatings.Add(userRating);
        await _context.SaveChangesAsync();
    }
}
