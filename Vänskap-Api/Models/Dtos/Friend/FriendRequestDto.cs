namespace Vänskap_Api.Models.Dtos.User
{
    public class FriendRequestDto
    {
        public IEnumerable<string?> IncomingRequests { get; set; } = [];
        public IEnumerable<string?> OutgoingRequests { get; set; } = [];
    }
}
