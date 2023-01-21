using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Instrumentation
{
    public class InstrumentationListItem
    {
        public int Id {get; set;}
        public string Instrument { get; set; }
        public int? CompositionId { get; set; }
    }
}