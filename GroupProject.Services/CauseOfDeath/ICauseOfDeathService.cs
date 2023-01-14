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
    Task<CauseOfDeathEntity> GetCauseByNameAsync(string cause);
    Task<bool> UpdateCauseAsync(CauseModel request);
    Task<bool> DeleteCauseAsync(int causeId);
    }
}