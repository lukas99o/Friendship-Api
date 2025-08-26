using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        public ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}