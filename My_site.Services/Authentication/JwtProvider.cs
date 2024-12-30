using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using My_site.DAL;
using My_site.DAL.Entities;
using My_site.Services.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_site.Services.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> options, ApplicationContext context) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;
        private readonly ApplicationContext _context = context;

        public string GenerateToken(UserEntity user)
        {
            var User = _context.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == user.Id);
            Claim[] claims = 
            [
                new(CustomClaims.UserId, User.Id.ToString()),
                new("Role", User.Roles.OrderBy(r => r.Id).FirstOrDefault()?.Id.ToString() ?? "2"),
                new("Username", user.Username.ToString()),
            ];

            var signingCredentails = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentails,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
