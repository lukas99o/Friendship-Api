using Microsoft.EntityFrameworkCore;
using Vänskap_Api.Models;
using Vänskap_Api.Service.IService;
using System.Security.Claims;
using Vänskap_Api.Data;
using Azure.Identity;
using Vänskap_Api.Models.Dtos.Conversation;

namespace Vänskap_Api.Service
{
    public class ConversationService : IConversationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string UserId => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException(nameof(UserId));

        public ConversationService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> StartConversation(List<string> userNames)
        {
            var user = await _context.Users.Include(f => f.Friendships)
                .ThenInclude(f => f.Friend).SingleOrDefaultAsync(u => u.Id == UserId);

            if (user == null || userNames.Count < 1) return false;

            var friendUserNames = user.Friendships
                .Select(f => f.Friend?.UserName).ToList();

            var allAreFriends = userNames.All(u => friendUserNames.Contains(u));
            if (!allAreFriends) return false;

            var friendNames = user.Friendships.Select(f => f.Friend?.FirstName).ToList();
            var friends = user.Friendships.ToList();

            if (friends.Count > 0)
            {
                if (friendNames.Count() > 0)
                {
                    var participants = new List<ConversationParticipant>();
                    var conversation = new Conversation() { Title = $"{user.FirstName}, {string.Join(", ", friendNames)}" };

                    await _context.AddAsync(conversation);
                    await _context.SaveChangesAsync();

                    participants.Add(new ConversationParticipant
                    {
                        UserId = UserId,
                        ConversationId = conversation.Id,
                        Role = "Host"
                    });
                    participants.AddRange(friends.Select(f => new ConversationParticipant
                    {
                        UserId = f.FriendId,
                        ConversationId = conversation.Id
                    }));

                    conversation.ConversationParticipants = participants;

                    _context.Update(conversation);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<string>> SeeAllConversations()
        {
            var userConversations = await _context.Conversations
                .Include(c => c.ConversationParticipants)
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId))
                .Select(c => c.Title)
                .ToListAsync();

            return userConversations;
        }

        public async Task<SeeConversationDto?> SeeConversation(int id)
        {
            var conversation = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId) && c.Id == id)
                .Include(c => c.Messages)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (conversation == null) return null;

            var seeConversation = new SeeConversationDto
            {
                Title = conversation.Title,
                Messages = conversation.Messages.Select(m => m.Content).ToList()
            };

            return seeConversation;
        }

        public async Task<bool> EditConversationTitle(int id, string title)
        {
            var conversation = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId ) && c.Id == id)
                .Include(c => c.ConversationParticipants)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (conversation == null) return false;

            var user = conversation.ConversationParticipants.SingleOrDefault(cp => cp.UserId == UserId);
            if (user == null) return false;

            if (user.Role != "Host") return false;

            conversation.Title = title;
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditRoles(int id, List<EditRoleDto> namesAndRoles)
        {
            var conversation = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId) && c.Id == id)
                .Include(c => c.ConversationParticipants)
                .ThenInclude(cp => cp.User)
                .SingleOrDefaultAsync();

            if (conversation == null) return false;

            var host = conversation.ConversationParticipants.SingleOrDefault(cp => cp.UserId == UserId);
            if (host == null || host.Role != "Host") return false;

            foreach (var dto in namesAndRoles)
            {
                if (dto.Role != "Host" || dto.Role != "Participant") continue;

                var participant = conversation.ConversationParticipants
                    .SingleOrDefault(cp => cp.User != null && cp.User.UserName == dto.UserName);

                if (participant != null)
                {
                    participant.Role = dto.Role;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveConversationParticipants(int id, List<string> userNames)
        {
            var conversation = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId) && c.Id == id)
                .Include(c => c.ConversationParticipants)
                .ThenInclude(cp => cp.User != null ? cp.User.UserName : "[Unknown user]") 
                .SingleOrDefaultAsync(c => c.Id == id);

            if (conversation == null) return false;

            var participantsToRemove = conversation.ConversationParticipants
                .Where(cp => cp.User != null && userNames.Contains(cp.User.UserName!))
                .ToList();

            if (!participantsToRemove.Any()) return false;

            _context.RemoveRange(participantsToRemove);

            if (conversation.ConversationParticipants.Count() < 3) return false;

            await _context.SaveChangesAsync();
            return true;
        }

        // The second paramter is all ursers that the host would like to make host if the user is the last host left
        public async Task<bool> RemoveUrselfFromConversation(int id, List<string?> userNames)
        {
            var conversation = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(cp => cp.UserId == UserId) && c.Id == id)
                .Include(c => c.ConversationParticipants)
                .SingleOrDefaultAsync();

            if (conversation == null) return false;
            var user = await _context.ConversationParticipants.SingleOrDefaultAsync(u => u.UserId == UserId);
            if (user == null) return false;
            var hosts = conversation.ConversationParticipants.Where(cp => cp.Role == "Host").ToList();
            var participants = conversation.ConversationParticipants.Where(cp => cp.Role == "Participant").ToList();
            var newHosts = new List<ConversationParticipant>();

            foreach (var participant in participants)
            {
                if (userNames.Contains(participant.User?.UserName)) newHosts.Add(participant);
            }

            if (hosts.Count() == 1)
            {
                foreach (var participant in newHosts)
                {
                    participant.Role = "Host";
                }

                _context.ConversationParticipants.Remove(user);

                if (conversation.ConversationParticipants.Count() < 1)
                {
                    _context.Conversations.Remove(conversation);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                _context.ConversationParticipants.Remove(user);

                if (conversation.ConversationParticipants.Count() < 1)
                {
                    _context.Conversations.Remove(conversation);
                }

                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SendMessage(string content, int id)
        {
            var isParticipant = await _context.ConversationParticipants
                .AnyAsync(cp => cp.ConversationId == id && cp.UserId == UserId);

            if (!isParticipant) return false;

            var message = new Message() { Content = content, ConversationId = id, SenderId = UserId };
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
