using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Vänskap_Api.Models.Dtos.Event;

namespace Vänskap_Api.Utilities
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(int eventId, string userName, string content)
        {
            var message = new EventSendMessageDto
            {
                SenderName = userName,
                Message = content,
                CreatedAt = DateTime.UtcNow
            };

            // Skicka till alla klienter som är anslutna till samma event-grupp
            await Clients.Group(eventId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var eventId = httpContext?.Request.Query["eventId"];

            if (!string.IsNullOrEmpty(eventId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, eventId!);
            }

            await base.OnConnectedAsync();
        }
    }

}
