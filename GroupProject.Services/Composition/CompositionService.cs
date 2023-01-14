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

        public async Task<bool> CreateCompositionAsync(CompositionCreate request)
            {
                var compositionEntity = new CompositionEntity
                {
                    Title = request.Title,
                    Composer = await _dbContext.Composers.FindAsync(request.ComposerId),
                    Genre = await _dbContext.Genres.FindAsync(request.GenreId),
                    Period = await _dbContext.Periods.FindAsync(request.PeriodId),
                    // Instruments = request.Instruments,
                };

                if(compositionEntity.Composer is null)
                return false;
                _dbContext.Add(compositionEntity);

                var numberOfChanges = await _dbContext.SaveChangesAsync();
                return numberOfChanges == 1;
            }

        public async Task<CompositionDetail?> GetCompositionByIdAsync(int Id) {
            var foundComposition = await _dbContext.Compositions
                .Include(entity=>entity.Composer)
                .Include(entity=>entity.Genre)
                .Include(entity=>entity.Period)
                .FirstOrDefaultAsync(comp => comp.Id == Id);
            
            return foundComposition is null 
                ? null
                : new CompositionDetail {
                    Id = foundComposition.Id,
                    Title = foundComposition.Title,
                    ComposerName = $"{foundComposition.Composer.FirstName} {foundComposition.Composer.LastName}",
                    OpusNumber = foundComposition.OpusNumber,
                    TotalViews = foundComposition.TotalViews,
                    DitterDorfs = foundComposition.DitterDorfs, 
                    GenreName = foundComposition.Genre.GenreName,
                    PeriodName = foundComposition.Period.Name
                    // // converts List<InstrumentEntity> into a list of instrument names ( List<string> )
                    // instruments = foundComposition.Instrumentation.Select(instrument => instrument.InstrumentName).ToList()
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

        //Takes in Composer Id, returns a list of Compositions by Composer.
        public async Task<IEnumerable<CompositionListItem>> GetAllCompositionsByComposerIdAsync(int composerId)
        {
            var compositions = await _dbContext.Compositions
                .Where(entity => composerId == entity.Composer.Id)
                .Select(entity => new CompositionListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    ComposerId = entity.Composer.Id
                })
                .ToListAsync();
            
            return compositions;
        }

        //Takes in Period Id, returns a list of Compositions by Composer.
        public async Task<IEnumerable<CompositionListItem>> GetAllCompositionsByPeriodIdAsync(int periodId)
        {
            var compositions = await _dbContext.Compositions
                .Where(entity => periodId == entity.Period.Id)
                .Select(entity => new CompositionListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    ComposerId = entity.Composer.Id
                })
                .ToListAsync();
            
            return compositions;
        }

        //Takes in Genre Id, returns a list of Compositions by Composer.
        public async Task<IEnumerable<CompositionListItem>> GetAllCompositionsByGenreIdAsync(int genreId)
        {
            var compositions = await _dbContext.Compositions
                .Where(entity => genreId == entity.Genre.Id)
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
            //todo ask terry
            // compositionEntity.Genre = request.GenreId;
            // compositionEntity.Composer = request.ComposerId;
            // compositionEntity.Period = request.PeriodId;

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