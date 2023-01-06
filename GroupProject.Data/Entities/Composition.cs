using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Data.Entities
{
    public class Composition
    {
        [Key]
        public int Id {get; set;}
        
        public string Title {get; set;}
        public string? OpusNumber {get; set;}
        [Required]
        public int PeriodId {get; set;}
        [Required]
        public int GenreId {get;set;}
        [Required]
        public int ComposerId {get;set;}
        public int TotalViews {get; set;}
        public int DitterDorfs {get; set;}

        public Composer Composer {get; set;}
        public Genre Genre {get; set;}
        public Period Period {get; set;}
        
        public List<Instrument> Instrumentation {get;set;}
    }
}