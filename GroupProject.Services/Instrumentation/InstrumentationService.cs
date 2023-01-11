using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Instrumentation;

namespace GroupProject.Services.Instrumentation

    public class InstrumentationService : IInstrumentationService
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext DbContext;
        public InstrumentationService(ApplicationDbContext context)
        {
            DbContext = context
        }
        
        public async Task<bool> CreateInstrumentationAsync(InstrumentationCreate request)
        {
            var instrumentationEntity = new InstrumentationEntity
            {

            };

            DbContext.Add(instrumentationEntity);

            var numberOfChanges = await DbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteInstrumentationAsync(int instrumentationId)
        {
            var instrumentationEntity = await _context.Instrumentations.FindAsync(instrumentationId);
            if (instrumentationEntity is null)
            return false;
            _context.Instrumentations.Remove(instrumentationEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<InstrumentationListItem>> GetAllInstrumentationAsync()
        {
            var instrumentations = await _context.Instrumentations
            .Where(entity => entity.Id == _instrumentationId)
            .Select(entity => new InstrumentationListItem
            {

            })
            .ToListAsync();

            return instrumentations;
        }

        public async Task<InstrumentationDetail> GetInstrumentationIdAsync(int instrumentationId)
        {
            var instrumentationEntity = await DbContext.Instrumentations.FindAsync(instrumentationId);

            return (instrumentationEntity is null) ? null : new InstrumentationDetail
            {
                Id = instrumentationEntity.Id
            };
        }

        public async Task<bool> UpdateInstrumentationAsync(InstrumentationUpdate request)
        {
            var instrumentationEntity = await _context.Instrumentations.FindAsync(request.Id);
            if(instrumentationEntity?.Id != _instrumentationId)
            return false;
            
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
