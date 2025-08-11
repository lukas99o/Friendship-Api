namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventReceiveMessageDto
    {
        public required string SenderId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
