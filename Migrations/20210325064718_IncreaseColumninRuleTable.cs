using Microsoft.EntityFrameworkCore.Migrations;

namespace _204362LibrarySystem.Migrations
{
    public partial class IncreaseColumninRuleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GetBook",
                table: "Rule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetBook",
                table: "Rule");
        }
    }
}
