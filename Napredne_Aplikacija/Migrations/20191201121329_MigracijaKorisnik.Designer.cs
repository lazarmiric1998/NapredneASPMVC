﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Napredne_Aplikacija.Models;

namespace Napredne_Aplikacija.Migrations
{
    [DbContext(typeof(KorisnikContext))]
    [Migration("20191201121329_MigracijaKorisnik")]
    partial class MigracijaKorisnik
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Napredne_Aplikacija.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Pol")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("KorisnikID");

                    b.ToTable("Korisnici");
                });
#pragma warning restore 612, 618
        }
    }
}