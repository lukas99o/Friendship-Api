using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class EventParticipant
    {
        [Key]
        public int Id { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public required string Role { get; set; } = "Participant";

        [Required]
        [ForeignKey("User")]
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public required Event Event { get; set; }
    }
}
