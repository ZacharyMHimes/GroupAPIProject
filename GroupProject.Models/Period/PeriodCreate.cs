using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Period
{
    public class PeriodCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Value must be at least {2} characters long.")]
        [MaxLength(50, ErrorMessage = "Value must contain no more than {50} characters.")]
        public string Name { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear {get; set;}
    }
}