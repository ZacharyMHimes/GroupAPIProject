using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class CompositionEntity
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

        public ComposerEntity Composer {get; set;}
        public GenreEntity Genre {get; set;}
        public PeriodEntity Period {get; set;}
        
        public List<InstrumentEntity> Instrumentation {get;set;}
    }
