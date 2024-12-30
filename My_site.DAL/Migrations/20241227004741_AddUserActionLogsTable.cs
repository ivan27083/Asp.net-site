using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace My_site.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserActionLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Computers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Url = table.Column<string>(type: "text", nullable: false),
            //        Name = table.Column<string>(type: "text", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: false),
            //        Price = table.Column<int>(type: "integer", nullable: false),
            //        GPU = table.Column<string>(type: "text", nullable: false),
            //        Rating = table.Column<double>(type: "double precision", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Computers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PermissionEntity",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PermissionEntity", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RolePermissionEntity",
            //    columns: table => new
            //    {
            //        RoleId = table.Column<int>(type: "integer", nullable: false),
            //        PermissionId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RolePermissionEntity", x => new { x.RoleId, x.PermissionId });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "UserActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying", nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Controller = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Method = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Details = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActionLogs", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Username = table.Column<string>(type: "text", nullable: false),
            //        PasswordHash = table.Column<string>(type: "text", nullable: false),
            //        Email = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PermissionEntityRoleEntity",
            //    columns: table => new
            //    {
            //        PermissionsId = table.Column<int>(type: "integer", nullable: false),
            //        RolesId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PermissionEntityRoleEntity", x => new { x.PermissionsId, x.RolesId });
            //        table.ForeignKey(
            //            name: "FK_PermissionEntityRoleEntity_PermissionEntity_PermissionsId",
            //            column: x => x.PermissionsId,
            //            principalTable: "PermissionEntity",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PermissionEntityRoleEntity_Roles_RolesId",
            //            column: x => x.RolesId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RoleEntityUserEntity",
            //    columns: table => new
            //    {
            //        RolesId = table.Column<int>(type: "integer", nullable: false),
            //        UsersId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleEntityUserEntity", x => new { x.RolesId, x.UsersId });
            //        table.ForeignKey(
            //            name: "FK_RoleEntityUserEntity_Roles_RolesId",
            //            column: x => x.RolesId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RoleEntityUserEntity_Users_UsersId",
            //            column: x => x.UsersId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_PermissionEntityRoleEntity_RolesId",
            //    table: "PermissionEntityRoleEntity",
            //    column: "RolesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleEntityUserEntity_UsersId",
            //    table: "RoleEntityUserEntity",
            //    column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Computers");

            //migrationBuilder.DropTable(
            //    name: "PermissionEntityRoleEntity");

            //migrationBuilder.DropTable(
            //    name: "RoleEntityUserEntity");

            //migrationBuilder.DropTable(
            //    name: "RolePermissionEntity");

            migrationBuilder.DropTable(
                name: "UserActionLogs");

            //migrationBuilder.DropTable(
            //    name: "PermissionEntity");

            //migrationBuilder.DropTable(
            //    name: "Roles");

            //migrationBuilder.DropTable(
            //    name: "Users");
        }
    }
}
