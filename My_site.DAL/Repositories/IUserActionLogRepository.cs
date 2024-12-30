using My_site.DAL.Entities;

namespace My_site.DAL.Repositories
{
    public interface IUserActionLogRepository
    {
        Task<IQueryable<UserActionLog>> GetAll();
        Task<Dictionary<string, int>> GetUserActionCounts();
        Task LogActionAsync(UserActionLog log);
    }
}