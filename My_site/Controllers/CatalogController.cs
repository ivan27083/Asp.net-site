﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_site.Domain.Attributes;
using My_site.Domain.Models;
using My_site.Services.Authentication;
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
        public async Task<IActionResult> GetItems([FromBody] CatalogFilterViewModel filter)
        {
            var response = await _service.GetAll(filter);
            if (response == null) return BadRequest(response);
            return Ok(response);
        }
    }
}
