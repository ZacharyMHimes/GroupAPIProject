using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composer
{
    public class ComposerDetail
    {
        public int Id {get; set;}
        public string FirstName {get;set;}
        public string LastName {get; set;}
        public string? Nationality {get; set;}
        public DateOnly? BirthDate {get; set;}
        public DateOnly? DeathDate {get; set;}
        //Call the Cause of Death View model here somehow?
        public int? SexyQuotientUpVotes {get;set;}
        public int? SexyQuotientTotalVotes {get; set;}  
    }
}