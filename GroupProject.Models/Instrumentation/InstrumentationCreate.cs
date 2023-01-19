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
        public int InstrumentId { get; set; }
        [Required]
        public int CompositionId { get; set; }
    }
}