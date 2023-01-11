using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Instrument
{
    public class InstrumentUpdate
    {
        public int Id {get; set;}
        public string InstrumentName {get; set;}
    }
}