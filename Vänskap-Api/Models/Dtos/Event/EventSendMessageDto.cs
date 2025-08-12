using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventSendMessageDto
    {
        public int EventId { get; set; }
        public required string Message { get; set; }
        public required string SenderName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
