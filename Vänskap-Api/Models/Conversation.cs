using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        [Required]
        public required ICollection<ConversationParticipant> ConversationParticipants { get; set; }

        [Required]
        public required ICollection<Message> Messages { get; set; } 
    }
}
