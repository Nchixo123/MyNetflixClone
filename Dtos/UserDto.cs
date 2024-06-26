using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Validations;

namespace Dtos
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "tinyint")]
        public Gender Gender { get; set; }
        [Required, MaxLength(255), EnglishLettersAndNumbers]
        public string Username { get; set; } = null!;
        [Required, MaxLength(255), EnglishLettersAndNumbers]
        public string Email { get; set; } = null!;
        [Required, MaxLength(255), EnglishLettersAndNumbers]
        public string Password { get; set; } = null!;
        [MaxLength(500)]
        public string ProfilePictureUrl { get; set; } = null!;
        [Required]
        public bool IsDelete { get; set; }

        public ICollection<MovieDto> FavoriteMovies { get; set; } = new List<MovieDto>();
    }

    public enum Gender : byte
    {
        Male = 0,
        Female = 1
    }
}
