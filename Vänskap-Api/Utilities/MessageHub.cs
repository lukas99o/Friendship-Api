using Microsoft.AspNetCore.SignalR;

namespace Vänskap_Api.Utilities
{
    public class MessageHub : Hub
    {
        public async Task JoinEventGroup(int eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, eventId.ToString());
            await Clients.Group(eventId.ToString()).SendAsync("SystemMessage", $"En ny användare har anslutit till event {eventId}.");
        }

        public async Task SendMessageToEvent(int eventId, string user, string message)
        {
            await Clients.Group(eventId.ToString()).SendAsync("ReceiveMessage", user, message);
        }

        public async Task LeaveEventGroup(int eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, eventId.ToString());
            await Clients.Group(eventId.ToString()).SendAsync("SystemMessage", $"En användare har lämnat event {eventId}.");
        }
    }
}
