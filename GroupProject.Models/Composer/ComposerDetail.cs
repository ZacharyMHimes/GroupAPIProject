using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composer
{
    public class ComposerDetail
    {
        public int Id {get; set;}
        public string? FirstName {get;set;}
        public string? LastName {get; set;}
        public string? Nationality {get; set;}
        public DateTime? BirthDate {get; set;}
        public DateTime? DeathDate {get; set;}
        public int? SexyQuotientUpVotes {get;set;}
        public int? SexyQuotientTotalVotes {get; set;}  
        public string? CauseOfDeath {get; set;}
    }
}