using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Genre
{
    public class GenreUpdate
    {
        public int Id {get; set;}

        public string? GenreName {get; set;}
    }
}