using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceInterfaces;

namespace MyNetflixClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(MovieModel movieModel)
    {
        var movieDto = new MovieDto
        {
            Title = movieModel.Title,
            Genre = movieModel.Genre,
            Description = movieModel.Description,
            Director = movieModel.Director,
            ImageUrl = movieModel.ImageUrl,
            VideoUrl = movieModel.VideoUrl,
            AverageRating = null
        };

        var createdMovie = await _movieService.CreateMovieAsync(movieDto);
        return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieModel movieModel)
    {
        var movieDto = await _movieService.GetMovieByIdAsync(id);
        if (movieDto == null)
        {
            return NotFound();
        }

        movieDto.Title = movieModel.Title;
        movieDto.Genre = movieModel.Genre;
        movieDto.Description = movieModel.Description;
        movieDto.Director = movieModel.Director;
        movieDto.ImageUrl = movieModel.ImageUrl;
        movieDto.VideoUrl = movieModel.VideoUrl;
        _ = await _movieService.UpdateMovieAsync(movieDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var deleted = await _movieService.DeleteMovieAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("top/{count}")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetTopRatedMovies(int count)
    {
        var movies = await _movieService.GetTopRatedMoviesAsync(count);
        return Ok(movies);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> SearchMovies(string keyword)
    {
        var movies = await _movieService.SearchMoviesAsync(keyword);
        return Ok(movies);
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByGenre(string genre)
    {
        var movies = await _movieService.GetMoviesByGenreAsync(genre);
        return Ok(movies);
    }

    [HttpPost("{movieId}/rate")]
    public async Task<IActionResult> AddUserRating(int movieId, int userId, decimal rating)
    {
        await _movieService.AddUserRatingAsync(movieId, userId, rating);
        return NoContent();
    }
}