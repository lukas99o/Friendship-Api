using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.Friend;
using Vänskap_Api.Models.Dtos.User;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Service
{
    public class FriendshipService : IFriendshipService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string UserId => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException(nameof(UserId));

        public FriendshipService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<GetFriendsDto>> SeeFriendList()
        {
            var user = await _context.Users
                .Include(u => u.Friendships)
                .ThenInclude(f => f.Friend)
                .SingleOrDefaultAsync(u => u.Id == UserId);

            var friendList = user?.Friendships
                .Select(f => f.Friend)
                .Where(f => f != null)
                .ToList();

            var friendListDto = friendList?.Select(f => new GetFriendsDto
            {
                Username = f.UserName,
                Name = f.FirstName + " " + f.LastName,
                Age = f.DateOfBirth,
                UserId = f.Id,
                ProfilePicturePath = f.ProfilePicturePath ?? string.Empty
            }).ToList();
            
            return friendListDto ?? new List<GetFriendsDto>();
        }

        public async Task<bool> SendFriendRequest(string username)
        {
            var friend = await _context.Users.Include(u => u.Friendships).SingleOrDefaultAsync(u => u.UserName == username);
            if (friend == null) return false;

            var isSent = await _context.FriendRequests.SingleOrDefaultAsync
                (u => u.SenderId == UserId && u.ReceiverId == friend.Id || u.SenderId == friend.Id && u.ReceiverId == UserId);
            if (isSent != null) return false;

            if (friend.Friendships.Any(f => f.FriendId == UserId)) return false;

            var friendRequest = new FriendRequest()
            {
                SenderId = UserId,
                ReceiverId = friend.Id
            };

            await _context.AddAsync(friendRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetFriendRequestsDto> SeeFriendRequests()
        {
            var incomingRequests = await _context.FriendRequests
                .Where(f => f.ReceiverId == UserId)
                .Include(f => f.Sender)
                .Select(f => f.Sender != null ? f.Sender.UserName : "[Unknown sender]")
                .ToListAsync();
            
            var outgoingRequests = await _context.FriendRequests
                .Where(f => f.SenderId == UserId)
                .Include(f => f.Receiver)
                .Select(f => f.Receiver != null ? f.Receiver.UserName : "[Unknown receiver]")
                .ToListAsync();

            var friendsRequests = new GetFriendRequestsDto()
            {
                IncomingUsernames = incomingRequests!,
                OutgoingUsernames = outgoingRequests!
            };

            return (friendsRequests);
        }

        public async Task<bool> AcceptFriendRequest(string username)
        {
            var friendRequest = await _context.FriendRequests
                .Include(fr => fr.Sender)
                .SingleOrDefaultAsync(fr => fr.Sender.UserName == username && fr.ReceiverId == UserId);

            if (friendRequest != null)
            {
                var friendshipOne = new Friendship() { UserId = UserId, FriendId = friendRequest.SenderId };
                var friendshipTwo = new Friendship() { UserId = friendRequest.SenderId, FriendId = UserId };

                var user = await _context.Users.FindAsync(UserId);
                var friend = await _context.Users.FindAsync(friendRequest.SenderId);
                if (user == null || friend == null) return false;

                user.Friendships.Add(friendshipOne);
                friend.Friendships.Add(friendshipTwo);

                _context.FriendRequests.Remove(friendRequest);
                await _context.Friendships.AddRangeAsync(friendshipOne, friendshipTwo);

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeclineFriendRequest(string username)
        {
            var friendRequest = await _context.FriendRequests
               .Include(fr => fr.Sender)
               .SingleOrDefaultAsync(fr => fr.Sender.UserName == username && fr.ReceiverId == UserId);

            if (friendRequest == null) return false;

            if (friendRequest.ReceiverId == UserId)
            {
                _context.Remove(friendRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<bool> RemoveFriend(string userName)
        {
            var friend = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (friend == null) return false;

            var friendshipOne = await _context.Friendships.Where(f => f.FriendId == friend.Id && f.UserId == UserId).SingleOrDefaultAsync();
            var friendshipTwo = await _context.Friendships.Where(f => f.FriendId == UserId && f.UserId == friend.Id).SingleOrDefaultAsync();
            if (friendshipOne == null || friendshipTwo == null) return false;

            _context.RemoveRange(friendshipOne, friendshipTwo);
            await _context.SaveChangesAsync();
                
            return true;
        }

        public async Task<bool> RemoveFriendRequest(string username)
        {
            var friendRequest = await _context.FriendRequests
                .Include(fr => fr.Receiver)
                .SingleOrDefaultAsync(fr => fr.Receiver.UserName == username && fr.SenderId == UserId); 
            if (friendRequest == null) return false;

            if (friendRequest.SenderId == UserId)
            {
                _context.Remove(friendRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
    }
}
