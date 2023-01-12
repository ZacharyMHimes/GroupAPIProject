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

        public async Task<IEnumerable<CompositionListItem>> GetAllCompositionsAsync()
        {
            var compositions = await _dbContext.Compositions
                .Select(entity => new CompositionListItem
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        ComposerId = entity.Composer.Id
                    })
                    .ToListAsync();

            return compositions;
        }

        public async Task<bool> UpdateCompositionAsync(CompositionUpdate request)
        {
            var compositionEntity = await _dbContext.Compositions.FindAsync(request.Id);
            compositionEntity.Id = request.Id;
            compositionEntity.Title = request.Title;
            compositionEntity.OpusNumber = request.OpusNumber;
            compositionEntity.TotalViews = request.TotalViews;
            compositionEntity.DitterDorfs = request.DitterDorfs;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        
        public async Task<bool> DeleteCompositionAsync(int Id)
        {
            var compositionEntity = await _dbContext.Composers.FindAsync(Id);
            _dbContext.Composers.Remove(compositionEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

    }
}