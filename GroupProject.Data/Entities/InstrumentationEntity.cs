using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class InstrumentationEntity
    {
        [Key]
        public int Id {get; set;}
        
        public InstrumentEntity Instrument {get; set;}
        public CompositionEntity Composition {get; set;}
    }
