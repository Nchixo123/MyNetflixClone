using Dtos;
using Models;
using Moq;
using RepoInterfaces;
using Repositories;
using ServiceInterfaces;
using Services;
using UnitTests.Fixtures;

namespace UnitTests;

public class MovieServiceTests : IClassFixture<LocalDb>
{
    private readonly NetflixDbContext _context;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IUserRatingService> _mockUserRatingService;
    private readonly MovieService _movieService;

    public MovieServiceTests(LocalDb fixture)
    {
        _context = fixture.Context;

        var movieRepositoryMock = new Mock<IMovieRepository>();
        var userRatingRepositoryMock = new Mock<IUserRatingsRepository>();

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(u => u.MovieRepository).Returns(movieRepositoryMock.Object);
        _mockUnitOfWork.Setup(u => u.UserRatingRepository).Returns(userRatingRepositoryMock.Object);
        _mockUserRatingService = new Mock<IUserRatingService>();

        _movieService = new MovieService(_mockUnitOfWork.Object, _mockUserRatingService.Object);
    }

    [Fact]
    public async Task AddMovieAsync_ShouldAddMovie()
    {
        // Arrange
        var movie = new MovieModel
        {
            Title = "New Movie",
            Description = "A horror movie",
            Director = "Me",
            Genre = "Horror",
            ImageUrl = "12",
            VideoUrl = "33"
        };


        // Act
        var result = await _movieService.CreateMovieAsync(movie);

        // Assert
        var moviesInDb = _context.Movies.ToList();
        Assert.Single(moviesInDb);
        Assert.Equal("New Movie", moviesInDb[0].Title);
    }

    [Fact]
    public async Task DeleteMovieAsync_ShouldReturn_False_IfNotFound()
    {
        // Arrange
        _mockUnitOfWork.Setup(u => u.MovieRepository.GetAsync(It.IsAny<int>())).Throws(new KeyNotFoundException());

        // Act
        var result = await _movieService.DeleteMovieAsync(1);

        // Assert
        Assert.False(result);
    }

    // Other test methods...

    [Fact]
    public async Task GetAllMoviesAsync_ShouldReturn_AllMovies()
    {
        // Arrange
        var movies = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Movie 1" },
            new MovieDto { Id = 2, Title = "Movie 2" }
        };
        _mockUnitOfWork.Setup(u => u.MovieRepository.SetAsync()).ReturnsAsync(movies);

        // Act
        var result = await _movieService.GetAllMoviesAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Movie 1", result.First().Title);
    }

    [Fact]
    public async Task GetMovieByIdAsync_ShouldReturn_Movie()
    {
        // Arrange
        var movie = new MovieDto { Id = 1, Title = "Movie 1" };
        _mockUnitOfWork.Setup(u => u.MovieRepository.GetAsync(1)).ReturnsAsync(movie);

        // Act
        var result = await _movieService.GetMovieByIdAsync(1);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Movie 1", result.Title);
    }

    [Fact]
    public async Task UpdateMovieAsync_ShouldUpdate_Movie()
    {
        // Arrange
        var movie = new MovieDto { Id = 1, Title = "Updated Movie" };

        // Act
        var result = await _movieService.UpdateMovieAsync(movie);

        // Assert
        _mockUnitOfWork.Verify(u => u.MovieRepository.Update(movie), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        Assert.Equal("Updated Movie", result.Title);
    }

    [Fact]
    public async Task GetTopRatedMoviesAsync_ShouldReturn_Movies()
    {
        // Arrange
        var movies = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Movie 1" },
            new MovieDto { Id = 2, Title = "Movie 2" }
        };
        _mockUnitOfWork.Setup(u => u.MovieRepository.GetTopRatedMoviesAsync(2)).ReturnsAsync(movies);

        // Act
        var result = await _movieService.GetTopRatedMoviesAsync(2);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Movie 1", result.First().Title);
    }

    [Fact]
    public async Task SearchMoviesAsync_ShouldReturn_Movies()
    {
        // Arrange
        var movies = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Keyword Movie 1" },
            new MovieDto { Id = 2, Title = "Another Movie 2" }
        };
        _mockUnitOfWork.Setup(u => u.MovieRepository.SearchMoviesAsync("Keyword")).ReturnsAsync(movies.Where(m => m.Title.Contains("Keyword")));

        // Act
        var result = await _movieService.SearchMoviesAsync("Keyword");

        // Assert
        Assert.Single(result);
        Assert.Equal("Keyword Movie 1", result.First().Title);
    }

    [Fact]
    public async Task GetMoviesByGenreAsync_ShouldReturn_Movies()
    {
        // Arrange
        var movies = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Movie 1", Genre = "Action" },
            new MovieDto { Id = 2, Title = "Movie 2", Genre = "Action" }
        };
        _mockUnitOfWork.Setup(u => u.MovieRepository.GetMoviesByGenreAsync("Action")).ReturnsAsync(movies.Where(m => m.Genre == "Action"));

        // Act
        var result = await _movieService.GetMoviesByGenreAsync("Action");

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, m => Assert.Equal("Action", m.Genre));
    }

    [Fact]
    public async Task AddUserRatingAsync_ShouldUpdateAverageRating()
    {
        // Arrange
        var userRating = new UserRatingsDto { MovieId = 1, UserId = 1, Rating = 5 };
        var movie = new MovieDto { Id = 1, Title = "Movie 1", AverageRating = 4 };

        _mockUnitOfWork.Setup(u => u.MovieRepository.GetAsync(1)).ReturnsAsync(movie);
        _mockUnitOfWork.Setup(u => u.UserRatingRepository.Set()).Returns(new List<UserRatingsDto> { userRating }.AsQueryable());

        // Act
        await _movieService.AddUserRatingAsync(1, 1, 5);

        // Assert
        _mockUnitOfWork.Verify(u => u.MovieRepository.Update(It.Is<MovieDto>(m => m.AverageRating == 5)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
