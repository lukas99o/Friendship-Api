using Vänskap_Api.Models.Dtos.Conversation;

namespace Vänskap_Api.Service.IService
{
    public interface IConversationService
    {
        Task<bool> StartConversation(List<string> userNames);
        Task<IEnumerable<string>> SeeAllConversations();
        Task<SeeConversationDto?> SeeConversation(int Id);
        Task<bool> EditConversationTitle(int id, string title);
        Task<bool> EditRoles(int id, List<EditRoleDto> namesAndRoles);
        Task<bool> RemoveConversationParticipants(int id, List<string> userNames);
        Task<bool> RemoveUrselfFromConversation(int id, List<string?> userNames);
        Task<bool> SendMessage(string content, int id);
    }
}
