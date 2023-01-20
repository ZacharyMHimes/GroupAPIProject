using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composition
{
    public class CompositionUpdateDitter
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [Range(-1, 1)]
        public int DitterDorfs {get; set;}
    }
}