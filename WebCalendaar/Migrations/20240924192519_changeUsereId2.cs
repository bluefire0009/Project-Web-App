using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class changeUsereId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "User",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Event_Attendance",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Attendance",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("26a83a6f-d7c1-4efa-bd16-8762a76a4b00"), "admin2@example.com", "\\N@6��G��Ae=j_��a%0�QU��\\", "admin2" },
                    { new Guid("90e75005-8700-4b14-87c0-627a2511073f"), "admin5@example.com", "E�=���:�-����gd����bF��80]�", "admin5" },
                    { new Guid("c35a95ed-e04e-44cb-97b6-ee9705ff4406"), "admin4@example.com", "�].��g��Պ��t��?��^�T��`aǳ", "admin4" },
                    { new Guid("c95cf1da-bd14-4ebe-a9d4-6d43044a9979"), "admin3@example.com", "�j\\��f������x�s+2��D�o���", "admin3" },
                    { new Guid("d3746ed0-ef40-4db8-995a-1ecb3573da31"), "admin1@example.com", "^�H��(qQ��o��)'s`=\rj���*�rB�", "admin1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("26a83a6f-d7c1-4efa-bd16-8762a76a4b00"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("90e75005-8700-4b14-87c0-627a2511073f"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("c35a95ed-e04e-44cb-97b6-ee9705ff4406"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("c95cf1da-bd14-4ebe-a9d4-6d43044a9979"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: new Guid("d3746ed0-ef40-4db8-995a-1ecb3573da31"));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "User",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Event_Attendance",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Attendance",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

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
    }
}
