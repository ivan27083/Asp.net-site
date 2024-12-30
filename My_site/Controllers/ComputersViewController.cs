using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_site.DAL;

namespace My_site.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class ComputersViewController : Controller
    {
        private readonly ApplicationContext _context;

        public ComputersViewController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
