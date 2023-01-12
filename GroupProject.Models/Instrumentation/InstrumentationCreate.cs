using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Instrumentation
{
    public class InstrumentationCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "{0} must have at least {1} ")]
        public string Instrument { get; set; }
        public int CompositionId { get; set; }
    }
}