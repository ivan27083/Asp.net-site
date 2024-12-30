﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_site.DAL.Entities;
using My_site.DAL.Repositories;
using My_site.Domain.Enums;

namespace My_site.DAL.Configurations
{
    public partial class RolePermissionConfiguration
        : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authorization;

        public RolePermissionConfiguration(AuthorizationOptions authorization)
        {
            _authorization = authorization;
        }
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new {r.RoleId, r.PermissionId});
            builder.HasData(ParseRolePermission());
        }
        private RolePermissionEntity[] ParseRolePermission() 
        { 
            return _authorization.RolePermissions
                .SelectMany(rp => rp.Permissions
                    .Select(p => new RolePermissionEntity
                    {
                        RoleId = (int)Enum.Parse<Role>(rp.Role),
                        PermissionId = (int)Enum.Parse<Permission>(p)
                    }))
                .ToArray();
        }
    }
}
