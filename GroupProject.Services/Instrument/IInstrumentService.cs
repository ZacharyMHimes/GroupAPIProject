using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Instrument;

namespace GroupProject.Services.Instrument
{
    public interface IInstrumentService
    {
        Task<bool> CreateInstrumentAsync(InstrumentCreate request);
        Task<IEnumerable<InstrumentListItem>> GetAllInstrumentsAsync();
        Task<InstrumentDetail?> GetInstrumentIdAsync(int instrumentId);
        Task<bool> UpdateInstrumentAsync(InstrumentUpdate request);
        Task<bool> DeleteInstrumentAsync(int instrumentId);
    }
}