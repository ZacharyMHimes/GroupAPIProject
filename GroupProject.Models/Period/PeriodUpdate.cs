using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Period
{
    public class PeriodUpdate
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime StartYear {get; set;}
        public DateTime EndYear {get; set;}
    }
}