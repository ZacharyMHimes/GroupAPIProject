using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.CauseOfDeath;

namespace GroupProject.Services.CauseOfDeath
{
    public interface ICauseOfDeathService
    {
    Task<bool> CreateCauseOfDeathAsync(CauseCreate request);
    Task<IEnumerable<CauseListItem>> GetAllCausesAsync(CauseListItem request);
    Task<CauseModel> GetCauseById(int causeId);
    Task<CauseModel> UpdateCause(int causeId);
    Task<bool> DeleteCause(int causeId);

    }
}