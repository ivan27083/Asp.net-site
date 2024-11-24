using Microsoft.AspNetCore.Mvc;
using My_site.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
namespace My_site.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
