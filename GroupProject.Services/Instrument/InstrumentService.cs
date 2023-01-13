using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Instrument;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Instrument
{
    public class InstrumentService : IInstrumentService
    {
        private readonly ApplicationDbContext _dbContext;

        public InstrumentService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> CreateInstrumentAsync(InstrumentCreate request)
        {
            var instrumentEntity = new InstrumentEntity
            {
                InstrumentName = request.InstrumentName
            };

            _dbContext.Add(instrumentEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<InstrumentListItem>> GetAllInstrumentsAsync()
        {
            var instruments = await _dbContext.Instruments
                .Select(entity => new InstrumentListItem
                    {
                        Id = entity.Id,
                        InstrumentName = entity.InstrumentName
                    })
                    .ToListAsync();

            return instruments;
        }

        public async Task<InstrumentDetail?> GetInstrumentIdAsync(int instrumentId)
        {
                var instrumentEntity = await _dbContext.Instruments.FirstOrDefaultAsync(e =>
                e.Id == instrumentId);
                return instrumentEntity is null ? null : new InstrumentDetail
                    {
                        Id = instrumentEntity.Id,
                        InstrumentName = instrumentEntity.InstrumentName
                    };
        }

        public async Task<bool> UpdateInstrumentAsync(InstrumentUpdate request)
        {
            var instrumentEntity = await _dbContext.Instruments.FindAsync(request.Id);
            
            instrumentEntity.InstrumentName = request.InstrumentName;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteInstrumentAsync(int Id)
        {
            var instrumentEntity = await _dbContext.Instruments.FindAsync(Id);
            if (instrumentEntity is null)
                return false;
            _dbContext.Instruments.Remove(instrumentEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return (numberOfChanges == 1);
        }
    }
}