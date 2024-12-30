

namespace My_site.Services.Services
{
    public interface IUserActionLogger
    {
        Task<Dictionary<string, int>> GetUserActionCounts();
        Task LogAsync(string username, string action, string controller, string method, string details = null);
    }
}