using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class ConversationParticipant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required Conversation Conversation { get; set; } 

        [Required]
        [ForeignKey("User")]
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; } 
    }
}
