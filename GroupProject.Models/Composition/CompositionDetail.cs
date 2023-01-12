using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composition
{
    public class CompositionDetail
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string ComposerName {get; set;}
        public string? OpusNumber {get; set;}
        public int TotalViews {get; set;}
        public int DitterDorfs {get; set;}
        public string GenreName {get; set;}
        public string PeriodName {get; set;}
        public List<string> instruments {get; set;}
    }
}