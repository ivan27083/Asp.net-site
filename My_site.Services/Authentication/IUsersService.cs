using My_site.DAL.Entities;

namespace My_site.Services.Authentication
{
    public interface IUsersService
    {
        Task<string> Login(string email, string password);
        Task Register(string userName, string email, string password);
        Task GiveRole(string email, int role);
        Task<bool> UpdateUser(UserEntity user);
        Task<bool> ChangePasswordAsync(string email, string newPassword);
        Task<UserEntity> GetByEmail(string email);
    }
}