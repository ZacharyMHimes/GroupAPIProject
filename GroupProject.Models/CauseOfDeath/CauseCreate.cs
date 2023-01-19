using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.CauseOfDeath
{
    public class CauseCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum Value must exceed {2} characters.")]
        public string CauseOfDeath {get; set;} 
    }
}