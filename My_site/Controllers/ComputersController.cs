using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_site.DAL;
using My_site.DAL.Entities;

namespace My_site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class ComputersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ComputersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Computers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerEntity>>> GetComputers()
        {
            return await _context.Computers.ToListAsync();
        }

        // GET: api/Computers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerEntity>> GetComputerEntity(int id)
        {
            var computerEntity = await _context.Computers.FindAsync(id);

            if (computerEntity == null)
            {
                return NotFound();
            }

            return computerEntity;
        }

        // PUT: api/Computers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerEntity(int id, ComputerEntity computerEntity)
        {
            if (id != computerEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(computerEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Computers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComputerEntity>> PostComputerEntity(ComputerEntity computerEntity)
        {
            _context.Computers.Add(computerEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComputerEntity", new { id = computerEntity.Id }, computerEntity);
        }

        // DELETE: api/Computers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerEntity(int id)
        {
            var computerEntity = await _context.Computers.FindAsync(id);
            if (computerEntity == null)
            {
                return NotFound();
            }

            _context.Computers.Remove(computerEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerEntityExists(int id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}
