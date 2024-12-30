using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_site.Services.Services;

namespace My_site.Controllers
{
    [Authorize(Policy = "UserPolicy")]
    public class LogsController : Controller
    {
        private readonly IUserActionLogger _userActionLogger;

        public LogsController(IUserActionLogger userActionLogger)
        {
            _userActionLogger = userActionLogger;
        }

        public async Task<IActionResult> Index()
        {
            var userActionCounts = await _userActionLogger.GetUserActionCounts();

            return View(userActionCounts);
        }
    }
}
