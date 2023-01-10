using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class ComposerEntity
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get; set;}
        public string? Nationality {get; set;}
        public DateOnly? BirthDate {get; set;}
        public DateOnly? DeathDate {get; set;}
        [Required]
        public int CauseOfDeathId {get; set;}
        public int? SexyQuotientUpVotes {get;set;} = 0;
        public int? SexyQuotientTotalVotes {get; set;} = 0;

        public CauseOfDeathEntity CauseOfDeath {get; set;}
        public List<CompositionEntity> Compositions {get; set;}
    }
