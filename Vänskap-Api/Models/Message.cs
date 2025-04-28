using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<DateTime>? ReadAts { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }
        public required Conversation Conversation { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public required string SenderId { get; set; }
        public required ApplicationUser Sender { get; set; }
    }
}
