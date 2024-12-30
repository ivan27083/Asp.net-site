using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_site.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserActionLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "UserActionLogs",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserActionLogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserActionLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "UserActionLogs",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserActionLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
