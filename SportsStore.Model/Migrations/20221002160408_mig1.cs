using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Itemname",
                table: "Stocks",
                newName: "ItemName");

            migrationBuilder.AddColumn<int>(
                name: "ThisItemId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThisItemId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Stocks",
                newName: "Itemname");
        }
    }
}
