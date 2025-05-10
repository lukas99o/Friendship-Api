using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
