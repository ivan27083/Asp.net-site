using Microsoft.EntityFrameworkCore;
using My_site.DAL.Entities;
using Npgsql;
using System.Data;

namespace My_site.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        ApplicationContext _context;
        public UsersRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task Add(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<UserEntity> GetByEmail(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            return user;
        }
        public async Task Update(UserEntity user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");
            }

            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }
        public async Task<HashSet<Permission>> GetUserPermissions(int userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles.
                SelectMany(r =>  r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
        public async Task GiveRole(int userId, int role)
        {
            var result = _context.Database.GetDbConnection().CreateCommand();
            result.CommandText = "INSERT INTO public.\"RoleEntityUserEntity\"(\"RolesId\", \"UsersId\") VALUES (@role, @userId)";
            result.Parameters.Add(new NpgsqlParameter("@role", role));
            result.Parameters.Add(new NpgsqlParameter("@userId", userId));
            if (result.Connection.State != ConnectionState.Open)
            {
                result.Connection.Open();
            }
            await result.ExecuteNonQueryAsync();
        }
    }
}
