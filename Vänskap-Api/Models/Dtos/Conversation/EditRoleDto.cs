namespace Vänskap_Api.Models.Dtos.Conversation
{
    public class EditRoleDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = "Participant";
    }
}
