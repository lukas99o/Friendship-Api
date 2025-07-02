namespace Vänskap_Api.Models.Dtos.Friend
{
    public class GetFriendRequestsDto
    {
        public List<string>? IncomingUserNames { get; set; }
        public List<string>? OutgoingUserNames { get; set; }
    }
}
