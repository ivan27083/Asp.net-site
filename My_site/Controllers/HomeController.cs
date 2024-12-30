using Microsoft.AspNetCore.Mvc;
using My_site.Models;
using My_site.Services.Implementations;
using System.Diagnostics;

namespace My_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComputerService _service;
        public HomeController(IComputerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var response = await _service.GetForMain();
            if (response == null) return BadRequest(response);
            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
