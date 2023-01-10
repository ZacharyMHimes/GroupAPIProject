using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Period;

namespace GroupProject.Services.Period
{
    public interface iPeriodServices
    {
        Task<bool> CreatePeriodAsync(PeriodCreate periodEntity);
        Task<PeriodDetail> GetPeriodAsync(int periodId);
    }
}