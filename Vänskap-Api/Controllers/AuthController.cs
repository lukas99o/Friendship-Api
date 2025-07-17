using Microsoft.AspNetCore.Identity;
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

            if (await _userManager.IsEmailConfirmedAsync(user)) 
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
            return Ok(new { Token = tokenString });
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"http://localhost:5173/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(user.Email, "Bekräfta din e-post",
                $"Klicka <a href=\"{confirmationLink}\">här</a> för att bekräfta din e-post.");

            var result = await _userManager.CreateAsync(user, register.Password);
            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors  = errors });
            }

            return Ok("Registrering lyckades");
        }

        [HttpGet("ConfirmEmail")] 
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Användare hittades inte.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Bekräftelsen misslyckades.");
            }

            return Ok("E-post bekräftad!");
        }
    }
}
