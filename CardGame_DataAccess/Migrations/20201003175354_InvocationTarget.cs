using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame_DataAccess.Migrations
{
    public partial class InvocationTarget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Set",
                table: "CardGame_Cards");

            migrationBuilder.AddColumn<string>(
                name: "When",
                table: "CardGame_Rules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvocationTarget",
                table: "CardGame_Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SetId",
                table: "CardGame_Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardGame_Sets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGame_Sets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardGame_Cards_SetId",
                table: "CardGame_Cards",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardGame_Cards_CardGame_Sets_SetId",
                table: "CardGame_Cards",
                column: "SetId",
                principalTable: "CardGame_Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardGame_Cards_CardGame_Sets_SetId",
                table: "CardGame_Cards");

            migrationBuilder.DropTable(
                name: "CardGame_Sets");

            migrationBuilder.DropIndex(
                name: "IX_CardGame_Cards_SetId",
                table: "CardGame_Cards");

            migrationBuilder.DropColumn(
                name: "When",
                table: "CardGame_Rules");

            migrationBuilder.DropColumn(
                name: "InvocationTarget",
                table: "CardGame_Cards");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "CardGame_Cards");

            migrationBuilder.AddColumn<int>(
                name: "Set",
                table: "CardGame_Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
