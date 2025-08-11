using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventSendMessage
    {
        public int EventId { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
