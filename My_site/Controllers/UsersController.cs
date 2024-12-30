using Microsoft.AspNetCore.Mvc;
using My_site.Services.Authentication;

namespace My_site.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpPost("Register")]
        public async Task<IResult> Register(string username, string password, string email, int role = 2)
        {
            await _usersService.Register(username, password, email);
            await _usersService.GiveRole(email, role);
            return Results.Ok();
        }
        [HttpPost("Login")]
        public async Task<IResult> Login(string email, string password)
        {
            var token = await _usersService.Login(email, password);
            if (token == null)
            {
                return Results.Unauthorized();
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                Expires = DateTime.Now.AddHours(12)
            };
            HttpContext.Response.Cookies.Append("token", token, cookieOptions);
            return Results.Ok();
        }
    }
}
