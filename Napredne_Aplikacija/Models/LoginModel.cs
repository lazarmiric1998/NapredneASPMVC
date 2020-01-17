using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Korisnicko ime")]
        public String KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        public String Sifra { get; set; }

        [Display (Name = "Zapamti me")]
        public bool rememberMe { get; set; }

        public string ReturnUrl { get; set; }
        // vise vrsta ext logna fb mc tw
        public IList<AuthenticationScheme> ExternalLogin { get; set; }

    }
}
