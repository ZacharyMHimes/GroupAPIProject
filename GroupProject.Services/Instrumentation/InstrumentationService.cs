using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Instrumentation;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Instrumentation
{
    public class InstrumentationService : IInstrumentationService
    {
        private readonly ApplicationDbContext _dbContext;
        public InstrumentationService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        
        public async Task<bool> CreateInstrumentationAsync(InstrumentationCreate request)
        {
            var instrumentationEntity = new InstrumentationEntity
            {
                Instrument = await _dbContext.Instruments.FindAsync(request.InstrumentId),
                Composition = await _dbContext.Compositions.FindAsync(request.CompositionId)
            };

            _dbContext.Add(instrumentationEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteInstrumentationAsync(int instrumentationId)
        {
            var instrumentationEntity = await _dbContext.Instrumentations.FindAsync(instrumentationId);
            if (instrumentationEntity is null)
            return false;
            _dbContext.Instrumentations.Remove(instrumentationEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<InstrumentationListItem>> GetAllInstrumentationAsync()
        {
            var instrumentations = await _dbContext.Instrumentations
            .Select(entity => new InstrumentationListItem
            {

            })
            .ToListAsync();

            return instrumentations;
        }

        public async Task<InstrumentationDetail?> GetInstrumentationIdAsync(int instrumentationId)
        {
            var instrumentationEntity = await _dbContext.Instrumentations.FindAsync(instrumentationId);

            return (instrumentationEntity is null) ? null : new InstrumentationDetail
            {
                Id = instrumentationEntity.Id
            };
        }

        public async Task<bool> UpdateInstrumentationAsync(InstrumentationUpdate request)
        {
            var instrumentationEntity = await _dbContext.Instrumentations.FindAsync(request.Id);
            
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
}
