using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Data.Entities
{
    public class InstrumentationEntity
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public int InstrumentId {get; set;}
        [Required]
        public int CompositionId {get; set;}
        public InstrumentEntity Instrument {get; set;}
        public CompositionEntity Composition {get; set;}
    }
}