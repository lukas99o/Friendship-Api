using Vänskap_Api.Models.Dtos.User;

namespace Vänskap_Api.Service.IService
{
    public interface IFriendshipService
    {
        Task<IEnumerable<string?>> SeeFriendList();
        Task<bool> SendFriendRequest(string userName);
        Task<(IEnumerable<string?> IncomingRequests, IEnumerable<string?> OutgoingRequests)> SeeFriendRequests();
        Task<bool> AcceptFriendRequest(int id);
        Task<bool> DeclineFriendRequest(int id);
        Task<bool> RemoveFriend(string friendId);
        Task<bool> RemoveFriendRequest(int id);
    }
}
