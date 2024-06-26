using Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);
            builder.Property(u => u.ProfilePictureUrl).HasMaxLength(500);
            builder.Property(u => u.Gender).IsRequired().HasConversion<byte>();
            builder.Property(u => u.IsDelete).IsRequired().HasDefaultValue(false);

            builder.HasMany(u => u.FavoriteMovies)
                   .WithMany(m => m.FavoritedByUsers)
                   .UsingEntity<UserRatingsDto>(
                        j => j.HasOne(ur => ur.Movie)
                              .WithMany(m => m.UserRatings)
                              .HasForeignKey(ur => ur.MovieId),
                        j => j.HasOne(ur => ur.User)
                              .WithMany(u => u.UserRatings)
                              .HasForeignKey(ur => ur.UserId));
        }
    }
}
