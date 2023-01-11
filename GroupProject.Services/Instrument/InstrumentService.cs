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
        private readonly ApplicationDbContext _context;
        private readonly int _instrumentId;
        private ApplicationDbContext DbContext;

        public InstrumentService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public async Task<bool> CreateInstrumentAsync(InstrumentCreate request)
        {
            var instrumentEntity = new InstrumentEntity
            {
                Id = _instrumentId,
                InstrumentName = request.InstrumentName
            };

            DbContext.Add(instrumentEntity);

            var numberOfChanges = await DbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<InstrumentListItem>> GetAllInstrumentsAsync()
        {
            var instruments = await _context.Instruments
                .Where(entity => entity.Id == _instrumentId)
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
                var instrumentEntity = await _context.Instruments.FirstOrDefaultAsync(e =>
                e.Id == instrumentId && e.Id == _instrumentId);
                return instrumentEntity is null ? null : new InstrumentDetail
                    {
                        Id = instrumentEntity.Id,
                        InstrumentName = instrumentEntity.InstrumentName
                    };
        }

        public async Task<bool> UpdateInstrumentAsync(InstrumentUpdate request)
        {
            var instrumentEntity = await _context.Instruments.FindAsync(request.Id);
            if(instrumentEntity?.Id != _instrumentId)
                return false;
            instrumentEntity.InstrumentName = request.InstrumentName;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteInstrumentAsync(int Id)
        {
            var instrumentEntity = await _context.Instruments.FindAsync(Id);
            if (instrumentEntity is null)
                return false;
            _context.Instruments.Remove(instrumentEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return (numberOfChanges == 1);
        }
    }
}