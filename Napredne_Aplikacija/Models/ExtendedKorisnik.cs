using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class ExtendedKorisnik : IdentityUser
    {
        public string City { get; set;}
        [NotMapped]
        public string EncryptedPin { get; set; }
        public string Pin { get; set;}
    }
}
