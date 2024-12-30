using My_site.DAL.Entities;

namespace My_site.DAL.Repositories
{
    public interface IComputerRepository
    {
        Task Create(ComputerEntity entity);
        Task Delete(ComputerEntity entity);
        IQueryable<ComputerEntity> GetAll();
        Task<ComputerEntity> Update(ComputerEntity entity);
    }
}