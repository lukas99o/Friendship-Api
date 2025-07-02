namespace Vänskap_Api.Models.Dtos.Conversation
{
    public class SeeConversationDto
    {
        public string? Title { get; set; }
        public IEnumerable<string>? Messages { get; set; }
    }
}
