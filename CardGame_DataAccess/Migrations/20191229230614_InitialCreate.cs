using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame_DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardGame_CardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardGame_Rules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    Effect = table.Column<string>(maxLength: 1000, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardGame_Subtypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_Subtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardGame_Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Trait = table.Column<string>(nullable: true),
                    Flavour = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    CardTypeId = table.Column<int>(nullable: false),
                    SubTypeId = table.Column<int>(nullable: true),
                    Attack = table.Column<int>(nullable: true),
                    Cooldown = table.Column<int>(nullable: true),
                    Health = table.Column<int>(nullable: true),
                    CostGreen = table.Column<int>(nullable: true),
                    CostWhite = table.Column<int>(nullable: true),
                    CostRed = table.Column<int>(nullable: true),
                    Rarity = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Set = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardGame_Cards_CardGame_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardGame_CardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardGame_Cards_CardGame_Subtypes_SubTypeId",
                        column: x => x.SubTypeId,
                        principalTable: "CardGame_Subtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardGame_CardRules",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false),
                    RuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_CardRules", x => new { x.CardId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_CardGame_CardRules_CardGame_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "CardGame_Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardGame_CardRules_CardGame_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "CardGame_Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardGame_CardRules_RuleId",
                table: "CardGame_CardRules",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CardGame_Cards_CardTypeId",
                table: "CardGame_Cards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardGame_Cards_SubTypeId",
                table: "CardGame_Cards",
                column: "SubTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardGame_CardRules");

            migrationBuilder.DropTable(
                name: "CardGame_Cards");

            migrationBuilder.DropTable(
                name: "CardGame_Rules");

            migrationBuilder.DropTable(
                name: "CardGame_CardTypes");

            migrationBuilder.DropTable(
                name: "CardGame_Subtypes");
        }
    }
}
