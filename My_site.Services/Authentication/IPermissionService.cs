
namespace My_site.Services.Authentication
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(int userId);
    }
}