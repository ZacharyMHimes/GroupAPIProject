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
    Task<IEnumerable<CauseListItem>> GetAllCausesAsync();
    Task<CauseModel> GetCauseIdAsync(int causeId);
    Task<CauseModel> UpdateCauseAsync(int causeId);
    Task<bool> DeleteCauseAsync(int causeId);
        Task<bool> UpdateCauseAsync(CauseModel model);
    }
}