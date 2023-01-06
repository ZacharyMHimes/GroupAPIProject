using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Data.Entities
{
    public class AdminEntity
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string UserName {get;set;}
        [Required]
        public string Password {get; set;}
    }
}