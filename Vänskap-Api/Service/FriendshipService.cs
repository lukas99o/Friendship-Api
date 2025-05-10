using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
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

        public async Task<IEnumerable<string?>> SeeFriendList()
        {
            var user = await _context.Users
                .Include(u => u.Friendships)
                .ThenInclude(f => f.Friend)
                .SingleOrDefaultAsync(u => u.Id == UserId);

            if (user == null) return Enumerable.Empty<string>();

            var friendList = user.Friendships
                .Select(f => f.Friend?.UserName)
                .Where(f => f != null)
                .ToList();
            
            return friendList;
        }

        public async Task<bool> SendFriendRequest(string userName)
        {
            var friend = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (friend == null) return false;

            var friendRequest = new FriendRequest()
            {
                SenderId = UserId,
                ReceiverId = friend.Id
            };

            return true;
        }

        public async Task<(IEnumerable<string?> IncomingRequests, IEnumerable<string?> OutgoingRequests)> SeeFriendRequests()
        {
            var incomingRequestTask = _context.FriendRequests
                .Where(f => f.ReceiverId == UserId)
                .Include(f => f.Sender)
                .Select(f => f.Sender != null ? f.Sender.UserName : "[Unknown sender]")
                .ToListAsync();
            
            var outgoingRequestTask = _context.FriendRequests
                .Where(f => f.SenderId == UserId)
                .Include(f => f.Receiver)
                .Select(f => f.Receiver != null ? f.Receiver.UserName : "[Unknown receiver]")
                .ToListAsync();

            await Task.WhenAll(incomingRequestTask, outgoingRequestTask);

            return (incomingRequestTask.Result, outgoingRequestTask.Result);
        }

        public async Task<bool> AcceptFriendRequest(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);

            if (friendRequest != null && friendRequest.ReceiverId == UserId)
            {
                var friendshipOne = new Friendship() { UserId = UserId, FriendId = friendRequest.SenderId };
                var friendshipTwo = new Friendship() { UserId = friendRequest.SenderId, FriendId = UserId };

                _context.FriendRequests.Remove(friendRequest);
                await _context.Friendships.AddRangeAsync(friendshipOne, friendshipTwo);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeclineFriendRequest(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);
            if (friendRequest == null) return false;

            if (friendRequest.ReceiverId == UserId)
            {
                _context.Remove(friendRequest);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
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

        public async Task<bool> RemoveFriendRequest(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);
            if (friendRequest == null) return false;

            if (friendRequest.SenderId == UserId)
            {
                _context.Remove(friendRequest);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
