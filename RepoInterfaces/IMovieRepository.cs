using Dtos;
namespace RepoInterfaces;

public interface IMovieRepository : IRepositoryBase<MovieDto>
{
    Task<IEnumerable<MovieDto>> GetTopRatedMoviesAsync(int count);
    Task<IEnumerable<MovieDto>> SearchMoviesAsync(string keyword);
    Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre);
}
