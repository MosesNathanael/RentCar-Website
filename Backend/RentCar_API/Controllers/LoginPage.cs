using Microsoft.AspNetCore.Mvc;
using RentCar_API.Data;
using RentCar_API.DTOs;
using RentCar_API.Models;
using Microsoft.EntityFrameworkCore;
using RentCar_API.PasswordHash;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RentCar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly RentCarDbContext _context;
        private readonly PasswordHasher _hash;
        private readonly IConfiguration _configuration;

        public LoginController(RentCarDbContext context, PasswordHasher hash, IConfiguration configuration)
        {
            _context = context;
            _hash = hash;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Get the full customer entity
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (customer == null)
                return Unauthorized("Email not found");

            // Validate password
            bool passwordValid = _hash.checkPassword(customer.Password, dto.Password);
            if (!passwordValid)
                return Unauthorized("Incorrect password");

            // JWT config
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            if (string.IsNullOrEmpty(key))  
            {
                return Unauthorized("JWT key is not configured");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.NameIdentifier, customer.Customer_id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return Ok(new LoginResponseModel
            {
                AccessToken = accessToken,
                UserName = customer.Name,
                UserEmail = customer.Email,
                UserId = customer.Customer_id,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            });
        }
    }
}
