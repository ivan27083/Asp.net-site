using My_site.DAL.Entities;
using System.Security.Claims;

namespace My_site.Services.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
        ClaimsPrincipal ValidateToken(string token);
    }
}