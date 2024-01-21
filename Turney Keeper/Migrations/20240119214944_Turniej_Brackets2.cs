using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turney_Keeper.Migrations
{
    public partial class Turniej_Brackets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BracketRound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurneyId = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BracketRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BracketRound_Turneys_TurneyId",
                        column: x => x.TurneyId,
                        principalTable: "Turneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BracketMatch",
                columns: table => new
                {
                    MatchNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    LoserId = table.Column<int>(type: "int", nullable: false),
                    BracketRoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BracketMatch", x => x.MatchNumber);
                    table.ForeignKey(
                        name: "FK_BracketMatch_BracketRound_BracketRoundId",
                        column: x => x.BracketRoundId,
                        principalTable: "BracketRound",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BracketMatch_BracketRoundId",
                table: "BracketMatch",
                column: "BracketRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketRound_TurneyId",
                table: "BracketRound",
                column: "TurneyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BracketMatch");

            migrationBuilder.DropTable(
                name: "BracketRound");
        }
    }
}
