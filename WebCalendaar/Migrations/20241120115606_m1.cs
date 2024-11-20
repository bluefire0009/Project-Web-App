using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCalendaar.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Event_Attendance",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Event_Attendance",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Event_Attendance");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Event_Attendance");
        }
    }
}
