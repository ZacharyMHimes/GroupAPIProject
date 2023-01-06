using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Data.Entities
{
    public class InstrumentEntity
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string InstrumentName {get; set;}
    }
}