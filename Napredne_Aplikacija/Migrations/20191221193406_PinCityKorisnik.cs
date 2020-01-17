using Microsoft.EntityFrameworkCore.Migrations;

namespace Napredne_Aplikacija.Migrations
{
    public partial class PinCityKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Korisnici",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "Korisnici",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Korisnici");
        }
    }
}
