using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Period;

namespace GroupProject.Services.Period
{
    public interface IPeriodService
    {
        Task<bool> CreatePeriodAsync(PeriodCreate periodEntity);
        Task<bool> DeletePeriodAsync(int periodId);
        Task<IEnumerable<PeriodEntityListItem>> GetAllPeriodAsync();
        Task<PeriodDetail> GetPeriodIdAsync(int periodId);
        Task<bool> UpdatePeriodAsync(PeriodUpdate request);
    }
}