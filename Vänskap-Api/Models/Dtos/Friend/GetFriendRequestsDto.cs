namespace Vänskap_Api.Models.Dtos.Friend
{
    public class GetFriendRequestsDto
    {
        public List<string>? IncomingUsernames { get; set; }
        public List<string>? OutgoingUsernames { get; set; }
    }
}
