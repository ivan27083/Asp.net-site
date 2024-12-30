using My_site.DAL.Entities;

namespace My_site.DAL.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly ApplicationContext _context;
       public ComputerRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public async Task Create(ComputerEntity entity)
        {
            await _context.Computers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ComputerEntity> GetAll()
        {
            return _context.Computers;
        }

        public async Task Delete(ComputerEntity entity)
        {
            _context.Computers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ComputerEntity> Update(ComputerEntity entity)
        {
            _context.Computers.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
