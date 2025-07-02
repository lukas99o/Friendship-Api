using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<MessageReadAt> ReadAts { get; set; } = new List<MessageReadAt>();
        public required string Content { get; set; }

        [ForeignKey("Conversation")]
        public int? ConversationId { get; set; }
        public Conversation? Conversation { get; set; }

        [ForeignKey("Sender")]
        public required string SenderId { get; set; }
        public ApplicationUser? Sender { get; set; }
    }
}
