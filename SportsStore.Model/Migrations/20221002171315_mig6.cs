using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Model.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageBoxId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageBoxId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Messages",
                newName: "SenderLname");

            migrationBuilder.RenameColumn(
                name: "Recipent",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.AddColumn<string>(
                name: "RecipentFName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipentId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipentLName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderFname",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MessageCount",
                table: "MessageBoxes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipentFName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipentLName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderFname",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageCount",
                table: "MessageBoxes");

            migrationBuilder.RenameColumn(
                name: "SenderLname",
                table: "Messages",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "Recipent");

            migrationBuilder.AddColumn<int>(
                name: "MessageBoxId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageBoxId",
                table: "Messages",
                column: "MessageBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages",
                column: "MessageBoxId",
                principalTable: "MessageBoxes",
                principalColumn: "Id");
        }
    }
}
