using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composition;

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
    }
}