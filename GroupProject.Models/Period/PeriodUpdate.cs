using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.Period
{
    public class PeriodUpdate
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [MinLength(2, ErrorMessage = "Value must be at least {2} characters long.")]
        [MaxLength(50, ErrorMessage = "Value must contain no more than {50} characters.")]
        public string Name {get; set;}
        public DateTime StartYear {get; set;}
        public DateTime EndYear {get; set;}
    }
}