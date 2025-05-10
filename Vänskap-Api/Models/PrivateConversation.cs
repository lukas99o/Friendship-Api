using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class PrivateConversation
    {
        [Key]
        public int Id { get; set; }

        public required string Title { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
