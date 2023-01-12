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
                    ComposerId = request.ComposerId,
                    GenreId = request.GenreId,
                    PeriodId = request.PeriodId,
                    Instruments = request.Instruments,
                };

                _dbContext.Add(compositionEntity);

                var numberOfChanges = await _dbContext.SaveChangesAsync();
                return numberOfChanges == 1;
            }
    }
}