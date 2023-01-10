using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {

        }

        public DbSet<AdminEntity> Admins {get; set;}
        public DbSet<CauseOfDeathEntity> DeathCauses {get;set;}
        public DbSet<ComposerEntity> Composers {get; set;}
        public DbSet<CompositionEntity> Compositions {get; set;}
        public DbSet<GenreEntity> Genres {get;set;}
        public DbSet<InstrumentationEntity> Instrumentations {get; set;}
        public DbSet<InstrumentEntity> Instruments {get;set;}
        public DbSet<PeriodEntity> Periods {get;set;}
    }
}