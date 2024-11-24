using Microsoft.AspNetCore.Mvc;
using My_site.DAL;
using My_site.DAL.Repositories;
using My_site.ViewModels;
using My_stie.Domainn.DomainModels;
using My_site.Services;
using My_site.Services.Implementations;

namespace My_site.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly IComputerService _service;
        public CatalogController(IComputerService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItems([FromBody]CatalogFilterViewModel filter)
        {
            var response = await _service.GetAll(filter);
            if (response == null) return BadRequest(response);
            return Ok(response);
        }
    }
}
