using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModularWebApi.Bootstrap;
using ModularWebApi.Modules.User.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ModularWebApi.Modules.User.Infrastructure
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JWTConfig _jwtConfig;
        public JWTProvider(IOptions<JWTConfig> jwtConfig) 
        {
            _jwtConfig = jwtConfig.Value;
        }
        public string GenerateToken(Guid userId, string role)
        {
            var signingKey = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.APIKey)), SecurityAlgorithms.HmacSha256
                );
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //var claims = new List<Claim>();
            //claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));

            var jwt = new JwtSecurityToken(
                    _jwtConfig.Issuer,
                    _jwtConfig.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signingKey
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.APIKey));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = _jwtConfig.Audience,
                    ValidIssuer = _jwtConfig.Issuer,
                    IssuerSigningKey = key
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
