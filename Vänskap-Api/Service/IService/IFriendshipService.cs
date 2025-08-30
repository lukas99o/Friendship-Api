using Vänskap_Api.Models.Dtos.Friend;
using Vänskap_Api.Models.Dtos.User;

namespace Vänskap_Api.Service.IService
{
    public interface IFriendshipService
    {
        Task<IEnumerable<GetFriendsDto>> SeeFriendList();
        Task<bool> SendFriendRequest(string username);
        Task<GetFriendRequestsDto> SeeFriendRequests();
        Task<bool> AcceptFriendRequest(string username);
        Task<bool> DeclineFriendRequest(int id);
        Task<bool> RemoveFriend(string friendId);
        Task<bool> RemoveFriendRequest(int id);
    }
}
