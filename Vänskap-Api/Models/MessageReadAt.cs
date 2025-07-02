using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class MessageReadAt
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReadAt { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public Message? Message { get; set; } 
    }
}
