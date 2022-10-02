using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemColor",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemInnerType",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ItemPrice",
                table: "Sales",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ItemSize",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SalsemanFname",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalsemanId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SalsemanLname",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ThisItemId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemColor",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemInnerType",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemSize",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SalsemanFname",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SalsemanId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SalsemanLname",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ThisItemId",
                table: "Sales");
        }
    }
}
