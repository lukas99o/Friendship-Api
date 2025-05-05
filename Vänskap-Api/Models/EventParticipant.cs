using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class EventParticipant
    {
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public required string UserName { get; set; }
        public string Role { get; set; } = "Participant";

        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
