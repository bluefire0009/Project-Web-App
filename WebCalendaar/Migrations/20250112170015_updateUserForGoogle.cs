using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class updateUserForGoogle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "auth_code",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "auth_token",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "auth_code",
                table: "User");

            migrationBuilder.DropColumn(
                name: "auth_token",
                table: "User");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "User");
        }
    }
}
