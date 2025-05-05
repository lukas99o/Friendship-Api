using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class ConversationParticipant
    {
        [Key]
        public int Id { get; set; }
        public required Conversation Conversation { get; set; } 

        [ForeignKey("User")]
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; } 
    }
}
