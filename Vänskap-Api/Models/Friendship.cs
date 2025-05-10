using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Friend")]
        public required string FriendId { get; set; }
        public ApplicationUser? Friend { get; set; }
    }
}
