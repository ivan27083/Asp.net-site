using Microsoft.AspNetCore.Authorization;

namespace My_site.Services.Authentication
{
    public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = permissions;
    }
}
