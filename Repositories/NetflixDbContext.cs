using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class NetflixDbContext : DbContext
    {
        public NetflixDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieDto>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.HasMany(m => m.UserRatings)
                      .WithOne(ur => ur.Movie)
                      .HasForeignKey(ur => ur.MovieId);

                entity.HasMany(m => m.FavoritedByUsers)
                      .WithMany(u => u.FavoriteMovies)
                      .UsingEntity<Dictionary<string, object>>(
                           "UserFavoriteMovies",
                           j => j.HasOne<UserDto>()
                                 .WithMany()
                                 .HasForeignKey("UserId")
                                 .OnDelete(DeleteBehavior.Cascade),
                           j => j.HasOne<MovieDto>()
                                 .WithMany()
                                 .HasForeignKey("MovieId")
                                 .OnDelete(DeleteBehavior.Cascade));
            });

            modelBuilder.Entity<UserDto>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasMany(u => u.FavoriteMovies)
                      .WithMany(m => m.FavoritedByUsers)
                      .UsingEntity<Dictionary<string, object>>(
                           "UserFavoriteMovies",
                           j => j.HasOne<MovieDto>()
                                 .WithMany()
                                 .HasForeignKey("MovieId")
                                 .OnDelete(DeleteBehavior.Cascade),
                           j => j.HasOne<UserDto>()
                                 .WithMany()
                                 .HasForeignKey("UserId")
                                 .OnDelete(DeleteBehavior.Cascade));
            });

            modelBuilder.Entity<UserRatingsDto>(entity =>
            {
                entity.HasKey(ur => ur.Id);

                entity.HasOne(ur => ur.User)
                      .WithMany()
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Movie)
                      .WithMany()
                      .HasForeignKey(ur => ur.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<MovieDto> Movies { get; set; }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<UserRatingsDto> UserRatings { get; set; }
    }
}
