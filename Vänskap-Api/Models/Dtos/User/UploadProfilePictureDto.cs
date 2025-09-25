using Microsoft.AspNetCore.Mvc;

namespace Vänskap_Api.Models.Dtos.User
{
    public class UploadProfilePictureDto
    {
        [FromForm(Name = "profilePicture")]
        public IFormFile ProfilePicture { get; set; } = null!;
    }
}
