
using My_site.ViewModels;
using My_stie.Domainn.DomainModels;

namespace My_site.Services.Implementations
{
    public interface IComputerService
    {
        Task<IEnumerable<Computer>> GetAll(CatalogFilterViewModel filter);
    }
}