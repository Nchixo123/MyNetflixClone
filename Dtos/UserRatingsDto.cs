using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validations;

namespace Dtos
{
    public class UserRatingsDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required, Column(TypeName = "decimal(3,2)"), RatingRange(1, 5)]
        public decimal Rating { get; set; }

        [ForeignKey("UserId")]
        public UserDto? User { get; set; }

        [ForeignKey("MovieId")]
        public MovieDto? Movie { get; set; }
    }
}