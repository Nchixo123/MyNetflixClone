using Dtos;
using Microsoft.EntityFrameworkCore;
using RepoInterfaces;

namespace Repositories;

internal sealed class MovieRepository : RepositoryBase<MovieDto>, IMovieRepository
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
}
