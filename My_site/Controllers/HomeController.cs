using Microsoft.AspNetCore.Mvc;
using My_site.Models;
using My_site.Services.Implementations;
using My_site.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace My_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IComputerService _service;
        public HomeController(ILogger<HomeController> logger, IComputerService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> GetItems()
        //{
        //    var response = await _service.GetForMain();
        //    if (response == null) return BadRequest(response);
        //    return Ok(response);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
