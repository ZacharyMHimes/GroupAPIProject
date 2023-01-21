using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Instrumentation;

namespace GroupProject.Services.Instrumentation
{
    public interface IInstrumentationService
    {
        Task<bool> CreateInstrumentationAsync(InstrumentationCreate instrumentationEntity);
        Task<bool> DeleteInstrumentationAsync(int instrumentationId);
        Task<IEnumerable<InstrumentationListItem>> GetAllInstrumentationAsync();
        Task<InstrumentationDetail> GetInstrumentationIdAsync(int instrumentationId);
        Task<bool> UpdateInstrumentationAsync(InstrumentationUpdate request);
    }
}