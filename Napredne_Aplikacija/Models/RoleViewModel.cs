using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class RoleViewModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
