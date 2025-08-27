namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventMessageDto
    {
        public int MessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
        public string? SenderId { get; set; }
        public string? SenderName { get; set; }
    }
}
