using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Genre
{
    public class GenreCreate
    {
        [Required]
        [MinLength(3, ErrorMessage = "Genre Name must have at least {3} characters ")]
        public string GenreName {get; set;}
    }
}