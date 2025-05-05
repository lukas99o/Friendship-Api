using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.User;

namespace Vänskap_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password!))
                return Unauthorized();
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtKey")!));
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
                FirstName = register.FirstName!,
                LastName = register.LastName!,
                DateOfBirth = register.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, register.Password!);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors  = errors });
            }

            return Ok("Registrering lyckades");
        }
    }
}
