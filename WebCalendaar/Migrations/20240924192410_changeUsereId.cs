using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class changeUsereId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("010727a9-0dd2-423c-81b4-14b2b197c943"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("25200a3c-e680-4953-896a-9d8993615cfb"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("9ac8b94c-c63c-4a8c-b7e2-567a0ed82bea"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("b67a3c79-30a2-4485-870f-d1dbb0e055c0"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("c29ba12b-16fc-4f82-be26-3d45be7a55e4"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("1508c550-b555-4741-9f03-76671744fc01"), "admin4@example.com", "�].��g��Պ��t��?��^�T��`aǳ", "admin4" },
                    { new Guid("6d580e58-ff9d-4fae-a69e-7d168e4cb213"), "admin3@example.com", "�j\\��f������x�s+2��D�o���", "admin3" },
                    { new Guid("7aa4e1b0-b477-4014-abc7-620940df033a"), "admin5@example.com", "E�=���:�-����gd����bF��80]�", "admin5" },
                    { new Guid("d47cdede-029f-4aa0-9bd4-ae0e3842661a"), "admin1@example.com", "^�H��(qQ��o��)'s`=\rj���*�rB�", "admin1" },
                    { new Guid("ef842aeb-d0c7-4de4-9f8d-4b03961e621c"), "admin2@example.com", "\\N@6��G��Ae=j_��a%0�QU��\\", "admin2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("1508c550-b555-4741-9f03-76671744fc01"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("6d580e58-ff9d-4fae-a69e-7d168e4cb213"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("7aa4e1b0-b477-4014-abc7-620940df033a"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("d47cdede-029f-4aa0-9bd4-ae0e3842661a"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("ef842aeb-d0c7-4de4-9f8d-4b03961e621c"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("010727a9-0dd2-423c-81b4-14b2b197c943"), "admin1@example.com", "^�H��(qQ��o��)'s`=\rj���*�rB�", "admin1" },
                    { new Guid("25200a3c-e680-4953-896a-9d8993615cfb"), "admin2@example.com", "\\N@6��G��Ae=j_��a%0�QU��\\", "admin2" },
                    { new Guid("9ac8b94c-c63c-4a8c-b7e2-567a0ed82bea"), "admin3@example.com", "�j\\��f������x�s+2��D�o���", "admin3" },
                    { new Guid("b67a3c79-30a2-4485-870f-d1dbb0e055c0"), "admin4@example.com", "�].��g��Պ��t��?��^�T��`aǳ", "admin4" },
                    { new Guid("c29ba12b-16fc-4f82-be26-3d45be7a55e4"), "admin5@example.com", "E�=���:�-����gd����bF��80]�", "admin5" }
                });
        }
    }
}
