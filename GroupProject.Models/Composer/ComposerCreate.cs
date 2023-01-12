using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composer
{
    public class ComposerCreate
    {
        //ID will be assigned by DB
        [Required]
        [MinLength
            (2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength
            (100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string FirstName {get;set;}
        [Required]
        [MinLength
            (2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength
            (100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string LastName {get; set;}
        public string? Nationality {get; set;}
        public DateTime? BirthDate {get; set;}
        public DateTime? DeathDate {get; set;}

        //ComposerEntity Will inherit Cause of Death from Cause of Death create MVC
        public int? SexyQuotientUpVotes {get;set;} = 0;
        public int? SexyQuotientTotalVotes {get; set;} = 0;

        //Composer will inherit Compositions from Composition MVC
    }
}
