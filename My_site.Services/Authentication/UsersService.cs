using Microsoft.Extensions.Primitives;
using My_site.DAL.Entities;
using My_site.DAL.Repositories;
using My_site.Domain.Models;

namespace My_site.Services.Authentication
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string userName, string password, string email)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var user = UserEntity.Create(userName, hashedPassword, email);
            await _usersRepository.Add(user);
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (result == false)
            {
                throw new Exception("Failed to login");
            }
            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
        public async Task GiveRole(string email, int role)
        {
            var user = await _usersRepository.GetByEmail(email);
            await _usersRepository.GiveRole(user.Id, role);
        }
        public async Task<bool> UpdateUser(UserEntity user)
        {
            var existingUser = await _usersRepository.GetByEmail(user.Email);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;

            await _usersRepository.Update(existingUser);

            return true;
        }
        public async Task<bool> ChangePasswordAsync(string email, string newPassword)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var newHashedPassword = _passwordHasher.Generate(newPassword);

            user.PasswordHash = newHashedPassword;

            await _usersRepository.Update(user);

            return true;
        }
        public async Task<UserEntity> GetByEmail(string email)
        {
            return await _usersRepository.GetByEmail(email);
        }
    }
}
