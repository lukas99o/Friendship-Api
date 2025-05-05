using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(72, MinimumLength = 8, ErrorMessage = "Password must be over 8 characters long.")]
        public string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
