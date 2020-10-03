using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame_DataAccess.Migrations
{
    public partial class Decks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardGame_Decks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_Decks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardGame_CardDecks",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false),
                    DeckId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_CardDecks", x => new { x.CardId, x.DeckId });
                    table.ForeignKey(
                        name: "FK_CardGame_CardDecks_CardGame_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "CardGame_Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardGame_CardDecks_CardGame_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "CardGame_Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardGame_CardDecks_DeckId",
                table: "CardGame_CardDecks",
                column: "DeckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardGame_CardDecks");

            migrationBuilder.DropTable(
                name: "CardGame_Decks");
        }
    }
}
