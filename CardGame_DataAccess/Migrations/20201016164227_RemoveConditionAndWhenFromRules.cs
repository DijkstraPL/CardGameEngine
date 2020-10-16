using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame_DataAccess.Migrations
{
    public partial class RemoveConditionAndWhenFromRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "CardGame_Rules");

            migrationBuilder.DropColumn(
                name: "When",
                table: "CardGame_Rules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "CardGame_Rules",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "When",
                table: "CardGame_Rules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
