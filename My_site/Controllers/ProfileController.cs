using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using My_site.DAL;
using My_site.DAL.Entities;
using My_site.Services.Authentication;
using My_site.Services.Services;
using System.Text.RegularExpressions;

namespace My_site.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IUsersService _usersService;
        private readonly IEmailService _emailService;

        public ProfileController(ApplicationContext applicationContext,
            IUsersService usersService,
            IEmailService emailService)
        {
            _applicationContext = applicationContext;
            _usersService = usersService;
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string email, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                ModelState.AddModelError("", "New password and confirmation do not match.");
                return BadRequest(new { message = "Passwords do not match." });
            }

            try
            {
                var result = await _usersService.ChangePasswordAsync(email, newPassword);
                if (!result)
                {
                    ModelState.AddModelError("", "Failed to change the password. Current password might be incorrect.");
                    return BadRequest(new { message = "Failed to change the password." });
                }

                return RedirectToAction("Index", "Home"); ;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while changing the password.");
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEntity model)
        {
            var username = HttpContext.Items["UserName"]?.ToString();
            var currentUser = _applicationContext.Users.AsNoTracking().FirstOrDefault(x => x.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("AuthorizationModal", "Authorization");
            }

            currentUser.Username = model.Username;
            currentUser.PasswordHash = model.PasswordHash;

            var updateResult = await _usersService.UpdateUser(currentUser);
            if (!updateResult)
            {
                ModelState.AddModelError("", "Failed to update profile. Please try again.");
                return View(model);
            }

            return RedirectToAction("Profile");
        }
        [HttpPost]
        public IActionResult ValidateEmail([FromBody] ValidateEmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || !Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return Json(new { isValid = false });
            }

            var user = _usersService.GetByEmail(request.Email);
            bool userExists;
            if (user == null)
            {
                userExists = false;
            }
            else userExists = true;
            return Json(new { isValid = userExists });
        }
        public class ValidateEmailRequest
        {
            public string Email { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> SendPasswordResetEmail(string email)
        {
            var user = await _usersService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            var resetLink = Url.Action("ChangePasswordModal", "Authorization", new { email = email }, Request.Scheme);
            await _emailService.SendAsync(email, "Password Reset", $"Click here to reset your password: {resetLink}");

            return Ok(new { message = "Password reset link has been sent." });
        }
        public IActionResult Index()
        {
            var username = HttpContext.Items["UserName"]?.ToString();
            var currentUser = _applicationContext.Users.AsNoTracking().FirstOrDefault(x => x.Username == username);
            if (currentUser == null)
            {
                return RedirectToAction("AuthorizationModal", "Authorization");
            }

            return View(currentUser);
        }
        public IActionResult Exit()
        {
            Response.Cookies.Delete("token");

            return RedirectToAction("Index", "Home");
        }
    }
}
