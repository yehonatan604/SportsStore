using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogType",
                table: "logsView");

            migrationBuilder.DropColumn(
                name: "LogType",
                table: "Logs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "logsView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
