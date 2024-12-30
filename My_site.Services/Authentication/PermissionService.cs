using My_site.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.Services.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public Task<HashSet<Permission>> GetPermissionsAsync(int userId)
        {
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}
