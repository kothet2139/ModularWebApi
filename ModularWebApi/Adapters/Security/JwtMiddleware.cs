using ModularWebApi.Modules.User.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace ModularWebApi.Adapters.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJWTProvider jWTProvider) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            if (!string.IsNullOrWhiteSpace(token) && jWTProvider.ValidateToken(token)) 
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "sub")?.Value;

                context.Items.Add("UserId", userId);
            }

            await _next(context);
        }
    }
}
