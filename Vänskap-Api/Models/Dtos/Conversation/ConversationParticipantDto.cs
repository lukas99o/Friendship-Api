namespace Vänskap_Api.Models.Dtos.Conversation
{
    public class ConversationParticipantDto
    {
        public int ConversationId { get; set; }
        public required string UserId { get; set; }
    }
}
