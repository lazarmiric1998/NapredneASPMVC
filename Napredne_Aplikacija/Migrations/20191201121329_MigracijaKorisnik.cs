using Microsoft.EntityFrameworkCore.Migrations;

namespace Napredne_Aplikacija.Migrations
{
    public partial class MigracijaKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "varchar(250)", nullable: false),
                    Prezime = table.Column<string>(type: "varchar(250)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Pol = table.Column<string>(type: "varchar(250)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "varchar(250)", nullable: false),
                    Sifra = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
