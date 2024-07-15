using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using MyNetflixClone.Interfaces;
using ServiceInterfaces;

namespace MyNetflixClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IS3Service _s3Service;

    public MovieController(IMovieService movieService, IS3Service s3Service)
    {
        _movieService = movieService;
        _s3Service = s3Service;
    }

    [HttpGet("movie")]
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

    [HttpPost("{title}/rate")]
    public async Task<IActionResult> AddUserRating(string title, int userId, decimal rating)
    {
        var movie = await _movieService.GetMovieByTitleAsync(title);
        if (movie == null)
        {
            return NotFound($"Movie with title {title} not found.");
        }

        await _movieService.AddUserRatingAsync(movie.Id, userId, rating);
        return NoContent();
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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadMovie(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var url = await _s3Service.UploadMovieAsync(stream, fileName);

        return Ok(new { Url = url });
    }

    [HttpGet("{id}/stream")]
    public async Task<IActionResult> StreamMovie(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        var secureUrl = _s3Service.GenerateSecureUrl(movie.VideoUrl);

        return Ok(new { Url = secureUrl });
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> FilterMovies([FromQuery] string genre, [FromQuery] decimal? minRating)
    {
        var movies = await _movieService.FilterMoviesAsync(genre, minRating);
        return Ok(movies);
    }

}