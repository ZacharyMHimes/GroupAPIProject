using System.Net.NetworkInformation;
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
            //separate request.Instruments by comma
            // e.g. "violin,bass,cello"
            var instrumentStringsToAdd = request.Instruments.Split(',');
            // make the compositionEntity
            var compositionEntity = new CompositionEntity
            {
                Title = request.Title,
                Composer = await _dbContext.Composers.FindAsync(request.ComposerId),
                Genre = await _dbContext.Genres.FindAsync(request.GenreId),
                Period = await _dbContext.Periods.FindAsync(request.PeriodId),
                Instrumentations = new List<InstrumentationEntity>()
            };

            if (compositionEntity.Composer is null)
                return false;
            _dbContext.Add(compositionEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            List<InstrumentEntity> instrumentEntities = new List<InstrumentEntity>();

            if (numberOfChanges > 0)
            {
                if (instrumentStringsToAdd is null || instrumentStringsToAdd.Length == 0) {
                    // compositionEntity.Instrumentations = new List<InstrumentationEntity>();
                    return true;
                }
                // check if each instrument exists already
                foreach (var instrumentName in instrumentStringsToAdd)
                {
                    var searchedInstrument = await _dbContext.Instruments.FirstOrDefaultAsync(i => i.InstrumentName == instrumentName);
                    // if not exists, create new instrument
                    if (searchedInstrument is null)
                    {
                        var newInstrument = new InstrumentEntity
                        {
                            InstrumentName = instrumentName
                        };
                        instrumentEntities.Add(newInstrument);
                        _dbContext.Instruments.Add(newInstrument);
                    }
                    else
                    {
                        instrumentEntities.Add(searchedInstrument);
                    }
                }
                numberOfChanges = await _dbContext.SaveChangesAsync();
                // create new instrumentation for each instrument we made/searched
                foreach (var instrument in instrumentEntities)
                {
                    _dbContext.Instrumentations.Add(new InstrumentationEntity
                    {
                        Instrument = instrument,
                        Composition = compositionEntity
                    });
                }
                numberOfChanges = await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CompositionDetail?> GetCompositionByIdAsync(int Id)
        {
            var foundComposition = await _dbContext.Compositions
                .Include(entity => entity.Composer)
                .Include(entity => entity.Genre)
                .Include(entity => entity.Period)
                .Include(entity => entity.Instrumentations)
                .ThenInclude(entity => entity.Instrument)
                .FirstOrDefaultAsync(comp => comp.Id == Id);

            return foundComposition is null
                ? null
                : new CompositionDetail
                {
                    Id = foundComposition.Id,
                    Title = foundComposition.Title,
                    ComposerName = $"{foundComposition.Composer.FirstName} {foundComposition.Composer.LastName}",
                    OpusNumber = foundComposition.OpusNumber,
                    TotalViews = foundComposition.TotalViews,
                    DitterDorfs = foundComposition.DitterDorfs,
                    GenreName = foundComposition.Genre?.GenreName,
                    PeriodName = foundComposition.Period?.Name,
                    // converts List<InstrumentEntity> into a list of instrument names ( List<string> )
                    instruments = foundComposition.Instrumentations.Select(instrumentation => instrumentation.Instrument.InstrumentName).ToList()
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
            compositionEntity.Genre =  await _dbContext.Genres.FindAsync(request.GenreId);
            compositionEntity.Composer = await _dbContext.Composers.FindAsync(request.ComposerId);
            compositionEntity.Period = await _dbContext.Periods.FindAsync(request.PeriodId);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        //Updates Likes and Views on a composition.
        public async Task<bool>UpdateCompositionDittersAndViewsAsync(CompositionUpdateDitterView request)
        {
            var compositionEntity = await _dbContext.Compositions.FindAsync(request.Id);
            compositionEntity.Id = request.Id;
            compositionEntity.TotalViews = compositionEntity.TotalViews + 1;
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