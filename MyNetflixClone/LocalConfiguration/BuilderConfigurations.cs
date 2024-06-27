using Microsoft.EntityFrameworkCore;
using RepoInterfaces;
using Repositories;
using ServiceInterfaces;
using Services;

namespace MyNetflixClone.LocalConfiguration
{
    public static class BuilderConfigurations
    {
        public static void ConfigureDependency(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IUserRatingService, UserRatingService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddDbContext<NetflixDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
