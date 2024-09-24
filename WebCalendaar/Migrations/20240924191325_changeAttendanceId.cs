using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class changeAttendanceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("6aee94d5-18ae-4c5f-8375-a01db02eb65c"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("6e6c00f1-465e-43f0-acec-2a3b7b95241c"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("a2c2272c-3110-4279-afcf-091cc67aa94a"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("dbaef662-5ec9-4d1f-8997-f437243d22fd"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("f37f2a15-d7a2-4d56-9c50-d5d007447bab"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendanceId",
                table: "Attendance",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceId",
                table: "Attendance",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("6aee94d5-18ae-4c5f-8375-a01db02eb65c"), "admin1@example.com", "^�H��(qQ��o��)'s`=\rj���*�rB�", "admin1" },
                    { new Guid("6e6c00f1-465e-43f0-acec-2a3b7b95241c"), "admin2@example.com", "\\N@6��G��Ae=j_��a%0�QU��\\", "admin2" },
                    { new Guid("a2c2272c-3110-4279-afcf-091cc67aa94a"), "admin3@example.com", "�j\\��f������x�s+2��D�o���", "admin3" },
                    { new Guid("dbaef662-5ec9-4d1f-8997-f437243d22fd"), "admin5@example.com", "E�=���:�-����gd����bF��80]�", "admin5" },
                    { new Guid("f37f2a15-d7a2-4d56-9c50-d5d007447bab"), "admin4@example.com", "�].��g��Պ��t��?��^�T��`aǳ", "admin4" }
                });
        }
    }
}
