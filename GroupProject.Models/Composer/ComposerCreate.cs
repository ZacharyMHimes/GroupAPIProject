using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Models.Composer
{
    public class ComposerCreate
    {
        
        public int Id {get; set;}

        public string FirstName {get;set;}

        public string LastName {get; set;}
        public string? Nationality {get; set;}
        public DateOnly? BirthDate {get; set;}
        public DateOnly? DeathDate {get; set;}

        public int CauseOfDeathId {get; set;}
        public int? SexyQuotientUpVotes {get;set;} = 0;
        public int? SexyQuotientTotalVotes {get; set;} = 0;

        public CauseOfDeathEntity CauseOfDeath {get; set;}
        public List<CompositionEntity> Compositions {get; set;}
    }
}