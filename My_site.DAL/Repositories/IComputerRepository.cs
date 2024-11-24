using My_stie.Domainn.DomainModels;

namespace My_site.DAL.Repositories
{
    public interface IComputerRepository
    {
        Task Create(Computer entity);
        Task Delete(Computer entity);
        IQueryable<Computer> GetAll();
        Task<Computer> Update(Computer entity);
    }
}