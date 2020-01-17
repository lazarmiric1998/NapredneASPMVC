using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        [Column(TypeName = "varchar(250)")]
        [Required]
        public String  Ime { get; set; }
        [Column(TypeName = "varchar(250)")]
        [Required]
        public String Prezime { get; set; }
        [Column(TypeName = "varchar(250)")]
        [Required]
        public String Email{ get; set; }
        [Column(TypeName = "varchar(250)")]
        public Pol Pol { get; set; }
        [Column(TypeName = "varchar(250)")]
        [Required]
        public String KorisnickoIme { get; set; }
        [Column(TypeName ="varchar(250)")]
        [DataType(DataType.Password)]
        [Required]
        public String  Sifra { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        [DisplayName("Potvrdi sifru")]
        [DataType(DataType.Password)]
        [Compare("Sifra")]
        public String PotvrdiSifru { get; set; }

        [Required]
        public String Grad { get; set; }
        [Required]
        public String Pin { get; set;}

    }
    public enum Pol { muski,zenski}
}
