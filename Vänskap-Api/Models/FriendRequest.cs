using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Sender")]
        public required string SenderId { get; set; }
        public ApplicationUser? Sender { get; set; }

        [ForeignKey("Receiver")]
        public required string ReceiverId { get; set; }
        public ApplicationUser? Receiver { get; set; }
    }
}
