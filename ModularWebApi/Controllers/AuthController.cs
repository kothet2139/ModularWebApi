using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ModularWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) 
        { 
            _config = config;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            if (loginRequest.Email == "test@gmail.com")
                return Ok(GenerateToken(loginRequest.Email));

            return Unauthorized();
        }

        private string GenerateToken(string userId)
        {
            var Issuer = _config["JWT:Issuer"];
            var Audience = _config["JWT:Audience"];

            var securityKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_config["JWT:APIKey"]!));
            var claims = new Dictionary<string, object>
            {
                { ClaimTypes.Email, userId}
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Audience = Audience,
                Issuer = Issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(30),
            };

            var tokenGenerator = new JsonWebTokenHandler();
            string token = tokenGenerator.CreateToken(securityTokenDescriptor);

            return token;
        }
    }
}
