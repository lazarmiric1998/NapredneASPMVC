using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Models
{
    public class EditRoleModel
    {
        public EditRoleModel()
        {
            Users = new List<string>();
        }
        public String Id { get; set; }
        [Required(ErrorMessage = "Ime role je obavezno")]
        public String RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
