using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthTokenDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "auth_token_time",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "auth_token_time",
                table: "User");
        }
    }
}
