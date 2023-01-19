using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.Composition
{
    public class CompositionUpdate
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [MinLength(3, ErrorMessage = "Composition must have at least {3} characters")]
        public string Title {get; set;}
        public string? OpusNumber {get; set;}
        public int? ComposerId {get; set;}
        public int? GenreId {get; set;}
        public int? PeriodId {get;set;}
        public int? TotalViews {get; set;}
        public int? DitterDorfs {get; set;}
    }
}