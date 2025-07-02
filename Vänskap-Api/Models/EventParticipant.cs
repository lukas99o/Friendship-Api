using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class EventParticipant
    {
        [Key]
        public int Id { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        public string Role { get; set; } = "Participant";

        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
