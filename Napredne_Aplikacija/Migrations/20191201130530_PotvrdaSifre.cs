using Microsoft.EntityFrameworkCore.Migrations;

namespace Napredne_Aplikacija.Migrations
{
    public partial class PotvrdaSifre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PotvrdiSifru",
                table: "Korisnici",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PotvrdiSifru",
                table: "Korisnici");
        }
    }
}
