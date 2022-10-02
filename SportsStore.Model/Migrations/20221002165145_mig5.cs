using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Sales");

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

            migrationBuilder.CreateTable(
                name: "SaleViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    ThisItemId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemInnerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemPrice = table.Column<double>(type: "float", nullable: false),
                    ItemColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalsemanId = table.Column<int>(type: "int", nullable: false),
                    SalsemanFname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalsemanLname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleViews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleViews");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemColor",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemInnerType",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItemPrice",
                table: "Sales",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemSize",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalsemanFname",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalsemanId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalsemanLname",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThisItemId",
                table: "Sales",
                type: "int",
                nullable: true);
        }
    }
}
