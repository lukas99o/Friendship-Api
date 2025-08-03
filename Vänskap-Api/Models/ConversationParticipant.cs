using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class ConversationParticipant
    {
        [Key]
        public int Id { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "Participant";

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }
        public Conversation? Conversation { get; set; } 

        [ForeignKey("User")]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; } 
    }
}
