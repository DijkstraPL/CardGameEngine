using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame_DataAccess.Migrations
{
    public partial class Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostWhite",
                table: "CardGame_Cards");

            migrationBuilder.AddColumn<int>(
                name: "CostBlue",
                table: "CardGame_Cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostBlue",
                table: "CardGame_Cards");

            migrationBuilder.AddColumn<int>(
                name: "CostWhite",
                table: "CardGame_Cards",
                type: "int",
                nullable: true);
        }
    }
}
