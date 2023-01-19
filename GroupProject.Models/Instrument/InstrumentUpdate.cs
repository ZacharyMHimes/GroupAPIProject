using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Instrument
{
    public class InstrumentUpdate
    {
        [Required]
        [MinLength(1, ErrorMessage = "{0} must have at least {1} ")]
        public int Id {get; set;}
        public string InstrumentName {get; set;}
    }
}