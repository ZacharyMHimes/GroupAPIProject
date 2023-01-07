using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Period
{
    public class PeriodDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
    }
}