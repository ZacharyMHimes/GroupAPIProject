using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class GenreEntity
    {
        [Key]
        public int Id {get; set;}
        public string GenreName {get; set;}
    }
