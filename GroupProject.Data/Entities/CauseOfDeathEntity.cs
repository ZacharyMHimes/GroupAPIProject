using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class CauseOfDeathEntity
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string Cause {get; set;}
    }
