using My_site.DAL.Entities;

namespace My_site.DAL.Repositories
{
    public interface IUsersRepository
    {
        Task Add(UserEntity user);
        Task<UserEntity> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(int userId);
        Task GiveRole(int userId, int role);
        Task Update(UserEntity user);
    }
}