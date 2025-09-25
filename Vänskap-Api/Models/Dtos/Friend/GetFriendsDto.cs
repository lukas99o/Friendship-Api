namespace Vänskap_Api.Models.Dtos.Friend
{
    public class GetFriendsDto
    {
        public string? Username { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public DateOnly Age { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}
