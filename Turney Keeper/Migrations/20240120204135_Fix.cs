using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turney_Keeper.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoserId",
                table: "BracketMatch");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "BracketMatch");

            migrationBuilder.RenameColumn(
                name: "WinnerScore",
                table: "BracketMatch",
                newName: "User2Score");

            migrationBuilder.RenameColumn(
                name: "LoserScore",
                table: "BracketMatch",
                newName: "User2Id");

            migrationBuilder.AddColumn<int>(
                name: "User1Id",
                table: "BracketMatch",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User1Score",
                table: "BracketMatch",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "BracketMatch");

            migrationBuilder.DropColumn(
                name: "User1Score",
                table: "BracketMatch");

            migrationBuilder.RenameColumn(
                name: "User2Score",
                table: "BracketMatch",
                newName: "WinnerScore");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "BracketMatch",
                newName: "LoserScore");

            migrationBuilder.AddColumn<int>(
                name: "LoserId",
                table: "BracketMatch",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "BracketMatch",
                type: "int",
                nullable: true);
        }
    }
}
