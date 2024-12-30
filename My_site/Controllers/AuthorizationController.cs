using Microsoft.AspNetCore.Mvc;

namespace My_site.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult AuthorizationModal()
        {
            return PartialView("~/Views/Shared/Authorization/_AuthorizationModal.cshtml");
        }
        public IActionResult RegistrationModal()
        {
            return PartialView("~/Views/Shared/Authorization/_RegistrationModal.cshtml");
        }
        public IActionResult ChangePasswordModal(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            return PartialView("~/Views/Shared/Authorization/_ChangePasswordModal.cshtml", email);
        }
    }
}
