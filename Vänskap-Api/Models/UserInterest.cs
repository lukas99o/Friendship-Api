namespace Vänskap_Api.Models
{
    public class UserInterest
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int InterestId { get; set; }
        public Interest? Interest { get; set; }
    }
}
