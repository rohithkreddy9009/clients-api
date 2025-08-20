using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        IConfiguration configuration;
        public TokenController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // GET: api/<TokenController>
        [HttpGet]
        public ActionResult<string> GetToken()
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, "user-123"),
                new(ClaimTypes.Name, "Username"),
                new(ClaimTypes.Email, "alice@example.com"),
                new(ClaimTypes.Role, "Admin"),
            };

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var expiryMinutes = int.TryParse(configuration["Jwt:ExpiryMinutes"], out var m) ? m : 60;

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return tokenString;
        }
    }
}
