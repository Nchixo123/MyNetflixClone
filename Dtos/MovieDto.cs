using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validations;

namespace Dtos;
public class MovieDto
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(255), EnglishLettersAndNumbers]
    public string Title { get; set; } = null!;
    [Required, MaxLength(100), EnglishLettersAndNumbers]
    public string Genre { get; set; } = null!;
    [MaxLength(1000), EnglishLettersAndNumbers]
    public string Description { get; set; } = null!;
    [MaxLength(255), EnglishLettersAndNumbers]
    public string Director { get; set; } = null!;
    [MaxLength(500)]
    public string ImageUrl { get; set; } = null!;
    [MaxLength(500)]
    public string VideoUrl { get; set; } = null!;
    [Column(TypeName = "decimal(3,2)")]
    public decimal? AverageRating { get; set; }

    public ICollection<UserRatingsDto> UserRatings { get; set; } = new List<UserRatingsDto>();
    public ICollection<UserDto> FavoritedByUsers { get; set; } = new List<UserDto>();
}
