﻿using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.User
{
    public class LoginDto
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
