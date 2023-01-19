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
        [MinLength
            (2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength
            (100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string? Nationality {get; set;}
        public DateTime? BirthDate {get; set;}
        public DateTime? DeathDate {get; set;}
        public int? SexyQuotientUpVotes {get;set;}
        public int? SexyQuotientTotalVotes {get; set;}
        [MinLength
            (2, ErrorMessage = "{0} must be at least {1} characters long.")]
        public string? CauseOfDeath {get; set;}

        

    }
}
