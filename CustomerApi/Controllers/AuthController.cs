using CustomerApi.IServices;
using CustomerApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbServices _db;

        public AuthController(IConfiguration configuration, IDbServices db)
        {
            _configuration = configuration;
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _db.GetAsync<Customer>(
                "sp_LoginUser2",
                new { Name = model.Name, Age = model.Age }
            );

            if (user == null)
                return Unauthorized("Invalid credentials");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
