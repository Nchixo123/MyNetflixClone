using Dtos;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataLibrary
{
    public class TestData
    {
        public static async Task Seed(NetflixDbContext context)
        {
  
            if (!context.Movies.Any())
            {
                var sampleMovies = new List<MovieDto>
            {
                new MovieDto
                {
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    Description = "A mind-bending thriller",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/7f/Inception_ver3.jpg",
                    VideoUrl = "https://www.example.com/inception.mp4",
                    AverageRating = 4.5M
                },
                new MovieDto
                {
                    Title = "The Dark Knight",
                    Genre = "Action",
                    Description = "A superhero film",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/8a/Dark_Knight.jpg",
                    VideoUrl = "https://www.example.com/darkknight.mp4",
                    AverageRating = 4.8M
                },
                new MovieDto
                {
                    Title = "Interstellar",
                    Genre = "Sci-Fi",
                    Description = "A space epic",
                    Director = "Christopher Nolan",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                    VideoUrl = "https://www.example.com/interstellar.mp4",
                    AverageRating = 4.7M
                },
                new MovieDto
                {
                    Title = "Parasite",
                    Genre = "Drama",
                    Description = "A dark comedy thriller",
                    Director = "Bong Joon-ho",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/53/Parasite_%282019_film%29.png",
                    VideoUrl = "https://www.example.com/parasite.mp4",
                    AverageRating = 4.6M
                },
                new MovieDto
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
}
