namespace Vänskap_Api.Models.Dtos.User
{
    public class UserDto
    {
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string ProfilePicturePath { get; set; } = null!;
        public string About { get; set; } = null!;
    }
}
