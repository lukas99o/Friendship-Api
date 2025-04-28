using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
