using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composer
{
    public class ComposerUpdateSexy
    {
        [Required]
        public int Id {get; set;}
        [Required]
        public bool IsSexy {get; set;}
    }
}