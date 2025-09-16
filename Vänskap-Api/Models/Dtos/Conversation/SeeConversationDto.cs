namespace Vänskap_Api.Models.Dtos.Conversation
{
    public class SeeConversationDto
    {
        public int ConversationId { get; set; }
        public string? Title { get; set; }
        public IEnumerable<ConversationMessageDto>? Messages { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ConversationParticipantDto>? ConversationParticipants { get; set; }
    }
}
