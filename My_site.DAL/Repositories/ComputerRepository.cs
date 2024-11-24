using My_stie.Domainn.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.DAL.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly ApplicationContext _context;
        public ComputerRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public async Task Create(Computer entity)
        {
            await _context.Computers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Computer> GetAll()
        {
            return _context.Computers;
        }

        public async Task Delete(Computer entity)
        {
            _context.Computers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Computer> Update(Computer entity)
        {
            _context.Computers.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
