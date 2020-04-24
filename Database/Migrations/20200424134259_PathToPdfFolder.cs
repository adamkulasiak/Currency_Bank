using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyBank.Database.Migrations
{
    public partial class PathToPdfFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathToPdfFolder",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToPdfFolder",
                table: "Users");
        }
    }
}
