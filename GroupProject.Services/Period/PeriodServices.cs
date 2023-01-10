using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Data.Entities;
using GroupProject.Models.Period;

namespace GroupProject.Services.Period
{
    public class PeriodServices : iPeriodServices
    {
    private readonly ApplicationDbContext _context;
    private ApplicationDbContext DbContext;

    public PeriodServices(ApplicationDbContext context)
    {
        DbContext = context;
    }
        public async Task<bool> CreatePeriodAsync(PeriodCreate request)
        {
            var periodEntity = new PeriodEntity
            {
                Name = request.Name,
                StartYear = request.StartYear,
            };

            DbContext.Add(periodEntity);

            var numberOfChanges = await DbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<PeriodDetail> GetPeriodAsync(int periodId)
        {
            var periodEntity = await DbContext.Periods.FindAsync(periodId);

            return (periodEntity is null) ? null : new PeriodDetail
            {
                Id = periodEntity.Id,
                Name = periodEntity.Name,
                StartYear = periodEntity.StartYear,
                EndYear = periodEntity.EndYear
            };
        }
    }
}