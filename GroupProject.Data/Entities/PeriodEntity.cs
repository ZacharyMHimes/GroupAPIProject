using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class PeriodEntity
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public DateOnly StartYear {get; set;}

        public DateOnly? EndYear {get; set;}
        public List<ComposerEntity> Composers {get; set;}
    }
