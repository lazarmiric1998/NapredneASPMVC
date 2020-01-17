using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class KorisnikContext:IdentityDbContext<ExtendedKorisnik>
    {
        public KorisnikContext(DbContextOptions<KorisnikContext> options) : base(options)
        {

        }

        public DbSet<Korisnik> Korisnici { get; set; }
    }
}
