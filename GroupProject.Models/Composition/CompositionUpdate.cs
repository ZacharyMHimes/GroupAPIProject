using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composition
{
    public class CompositionUpdate
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string? OpusNumber {get; set;}
        
        public int TotalViews {get; set;}
        public int DitterDorfs {get; set;}
    }
}