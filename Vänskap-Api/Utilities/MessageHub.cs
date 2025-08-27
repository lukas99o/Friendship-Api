using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Vänskap_Api.Data;
using Vänskap_Api.Models;

namespace Vänskap_Api.Utilities
{
    public class MessageHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public MessageHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
            await Clients.Group(conversationId.ToString()).SendAsync("SystemMessage", $"En användare har anslutit.");
        }

        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
            await Clients.Group(conversationId.ToString()).SendAsync("SystemMessage", $"En användare har lämnat.");
        }

        public async Task SendMessage(int conversationId, string senderId, string content)
        {
            var participant = await _context.ConversationParticipants
                .FirstOrDefaultAsync(cp => cp.ConversationId == conversationId && cp.UserId == senderId);

            if (participant == null)
            {
                await Clients.Caller.SendAsync("Error", "Du är inte deltagare i denna konversation.");
                return;
            }

            var message = new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                Content = content
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await Clients.Group(conversationId.ToString()).SendAsync(
                "ReceiveMessage",
                new
                {
                    message.Id,
                    message.Content,
                    message.SenderId,
                    message.CreatedAt
                });
        }
    }
}
