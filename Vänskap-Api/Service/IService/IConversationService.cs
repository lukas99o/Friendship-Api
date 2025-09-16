using Vänskap_Api.Models.Dtos.Conversation;

namespace Vänskap_Api.Service.IService
{
    public interface IConversationService
    {
        Task<SeeConversationDto?> StartPrivateConversation(string username);
        Task<IEnumerable<SeeConversationDto>> SeeAllConversations();
        Task<SeeConversationDto?> SeeConversation(int Id);
        Task<IEnumerable<ConversationMessageDto>> GetConversationMessages(int id);
        Task<bool> EditConversationTitle(int id, string title);
        Task<bool> EditRoles(int id, List<EditRoleDto> namesAndRoles);
        Task<bool> RemoveConversationParticipants(int id, List<string> userNames);
        Task<bool> RemoveUrselfFromConversation(int id, List<string?> userNames);
        Task<bool> SendMessage(string content, int id);
    }
}
