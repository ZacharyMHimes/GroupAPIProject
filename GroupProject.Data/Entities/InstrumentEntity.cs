using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class InstrumentEntity
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string InstrumentName {get; set;}
        public List<InstrumentationEntity>? instrumentations {get; set;}
    }