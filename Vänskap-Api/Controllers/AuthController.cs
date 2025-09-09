﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.User;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
 
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password) || string.IsNullOrEmpty(user.UserName))
                return Unauthorized();

            if (!await _userManager.IsEmailConfirmedAsync(user)) 
                return Unauthorized("Du måste bekräfta din e-post först.");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? throw new InvalidOperationException("JwtKey enviroment variable is not set.")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JwtIssuer"),
                audience: Environment.GetEnvironmentVariable("JwtAudience"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString, Username = user.UserName, UserId = user.Id });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.UserName,
                FirstName = register.FirstName,
                LastName = register.LastName,
                DateOfBirth = register.DateOfBirth
            }; 

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.ToList();

                if (errors.Any(e => e.Code == "DuplicateUserName"))
                {
                    return BadRequest("Användarnamnet är redan taget.");
                }

                if (errors.Any(e => e.Code == "DuplicateEmail"))
                {
                    return BadRequest("E-postadressen används redan.");
                }

                var allErrors = errors.Select(e => e.Description);
                return BadRequest(new { message = "Registrering misslyckades.", errors = allErrors });
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{Environment.GetEnvironmentVariable("BaseUrl")}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var htmlTemplate = await System.IO.File.ReadAllTextAsync("EmailTemplates/ConfirmEmail.html");
            var htmlBody = htmlTemplate
                .Replace("{FirstName}", user.FirstName)
                .Replace("{confirmationLink}", confirmationLink);

            await _emailService.SendEmailAsync(user.Email, "Bekräfta din e-post", htmlBody);

            return Ok("Registrering lyckades");
        }

        [HttpPost("resend-email")]
        public async Task<IActionResult> ResendConfirmationLink(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("Användare hittades inte.");
            }
            else
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{Environment.GetEnvironmentVariable("BaseUrl")}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
                var htmlTemplate = await System.IO.File.ReadAllTextAsync("EmailTemplates/ConfirmEmail.html");
                var htmlBody = htmlTemplate
                    .Replace("{FirstName}", user.FirstName)
                    .Replace("{confirmationLink}", confirmationLink);

                await _emailService.SendEmailAsync(user.Email!, "Bekräfta din e-post för att registrera dig i Vänskap", htmlBody);

                return Ok("Bekräftelse mail skickat! Kontrollera din inkorg. Om du inte ser det, kolla skräpposten.");
            }
        }

        [HttpPost("Confirm-Email")] 
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Användare hittades inte.");

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                return BadRequest("Bekräftelsen misslyckades.");
            }

            return Ok("E-post bekräftad!");
        }
    }
}
