using My_site.DAL.Entities;
using My_site.Domain.Models;

namespace My_site.Services.Implementations
{
    public interface IComputerService
    {
        Task<IEnumerable<ComputerEntity>> GetAll(CatalogFilterViewModel filter);
        Task<IEnumerable<ComputerEntity>> GetForMain();
    }
}