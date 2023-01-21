using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Admin
{
    public class AdminUpdate
    {
        [Required]
        public int Id {get; set;}
        
        [MinLength(1)]
        public string Username {get; set;}
        [Required]
        [MinLength(8)]
        public string Password {get; set;}
    }
}