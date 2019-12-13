using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyBank.Database.Migrations
{
    public partial class AddedLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Language",
                table: "Users",
                nullable: true,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");
        }
    }
}
