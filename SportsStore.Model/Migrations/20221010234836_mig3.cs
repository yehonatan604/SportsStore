using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerFname",
                table: "SaleViews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SaleViews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerLname",
                table: "SaleViews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerFname",
                table: "SaleViews");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SaleViews");

            migrationBuilder.DropColumn(
                name: "CustomerLname",
                table: "SaleViews");
        }
    }
}
