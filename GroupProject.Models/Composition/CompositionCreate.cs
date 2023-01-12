using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composition
{
    public class CompositionCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "{0} must have at least {1} ")]
        public string Title { get; set; }
        public int ComposerId { get; set; }
        public int GenreId { get; set; }
        public int PeriodId { get; set; }
        public string Instruments { get; set; }
        
    }
}