using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composition;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Composition
{
    public class CompositionService : ICompositionService
    {
        private readonly ApplicationDbContext _dbContext;
        public CompositionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CompositionDetail?> GetCompositionByIdAsync(int Id) {
            var foundComposition = await _dbContext.Compositions.FirstOrDefaultAsync(comp=> comp.Id == Id);
            
            return (foundComposition is null) 
                ? null
                : new CompositionDetail {
                    Id = foundComposition.Id,
                    Title = foundComposition.Title,
                    ComposerName = $"{foundComposition.Composer.FirstName} {foundComposition.Composer.LastName}",
                    OpusNumber = foundComposition.OpusNumber,
                    TotalViews = foundComposition.TotalViews,
                    DitterDorfs = foundComposition.DitterDorfs, 
                    GenreName = foundComposition.Genre.GenreName,
                    PeriodName = foundComposition.Period.Name,
                    // converts List<InstrumentEntity> into a list of instrument names ( List<string> )
                    instruments = foundComposition.Instrumentation.Select(instrument => instrument.InstrumentName).ToList()
                };
        }
    }
}