// SeederData.cs
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SeederData
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NetflixDbContext>();

        if (!context.Movies.Any())
        {
            var sampleMovies = new List<Movie>
            {
                new Movie
                {
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    Description = "A mind-bending thriller",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/7f/Inception_ver3.jpg",
                    VideoUrl = "https://www.example.com/inception.mp4",
                    AverageRating = 4.5M
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Genre = "Action",
                    Description = "A superhero film",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/8a/Dark_Knight.jpg",
                    VideoUrl = "https://www.example.com/darkknight.mp4",
                    AverageRating = 4.8M
                },
                new Movie
                {
                    Title = "Interstellar",
                    Genre = "Sci-Fi",
                    Description = "A space epic",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                    VideoUrl = "https://www.example.com/interstellar.mp4",
                    AverageRating = 4.7M
                },
                new Movie
                {
                    Title = "Parasite",
                    Genre = "Drama",
                    Description = "A dark comedy thriller",
                    Director = "Bong Joon-ho",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/53/Parasite_%282019_film%29.png",
                    VideoUrl = "https://www.example.com/parasite.mp4",
                    AverageRating = 4.6M
                },
                new Movie
                {
                    Title = "Avengers: Endgame",
                    Genre = "Action",
                    Description = "A superhero film",
                    Director = "Anthony and Joe Russo",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/0/0d/Avengers_Endgame_poster.jpg",
                    VideoUrl = "https://www.example.com/endgame.mp4",
                    AverageRating = 4.9M
                }
            };

            await context.Movies.AddRangeAsync(sampleMovies);
            await context.SaveChangesAsync();
        }
    }
}
