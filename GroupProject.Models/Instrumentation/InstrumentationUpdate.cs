using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Instrumentation
{
    public class InstrumentationUpdate
    {
        [Required]
        public int Id {get; set;}
        [Required]
        public string Instrument { get; set; }
        [Required]
        public int CompositionId { get; set; }
    }
}