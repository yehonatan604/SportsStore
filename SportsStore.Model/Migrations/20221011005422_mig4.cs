using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "SaleViews",
                newName: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "SaleViews",
                newName: "ItemId");
        }
    }
}
