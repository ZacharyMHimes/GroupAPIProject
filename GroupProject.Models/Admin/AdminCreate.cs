using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Admin
{
    public class AdminCreate
    {
        [Required]
        public string Username {get; set;}
        [Required]
        [MinLength(8, ErrorMessage = "{0} must be at least {1} characters long")]
        public string Password {get; set;}
    }
}