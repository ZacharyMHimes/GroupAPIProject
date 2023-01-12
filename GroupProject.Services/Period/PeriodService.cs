using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Period;
using GroupProject.Services.Period;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Period
{
public class PeriodService : IPeriodService
    {

    private readonly ApplicationDbContext _dbContext;

    public PeriodService(ApplicationDbContext context)
    {
        _dbContext = context;
    }
        public async Task<bool> CreatePeriodAsync(PeriodCreate request)
        {
            var periodEntity = new PeriodEntity
            {
                Name = request.Name,
                StartYear = request.StartYear,
            };

            _dbContext.Add(periodEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> UpdatePeriodAsync(PeriodUpdate request)
        {
            var periodEntity = await _dbContext.Periods.FindAsync(request.Id);
            periodEntity.Name = request.Name;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

            public async Task<bool> DeletePeriodAsync(int Id)
        {
            var periodEntity = await _dbContext.Periods.FindAsync(Id);
            if (periodEntity is null)
                return false;
            _dbContext.Periods.Remove(periodEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

            public async Task<PeriodDetail?> GetPeriodIdAsync(int periodId)
        {
            var periodEntity = await _dbContext.Periods.FindAsync(periodId);

            return (periodEntity is null) ? null : new PeriodDetail
            {
                Id = periodEntity.Id,
                Name = periodEntity.Name,
                StartYear = periodEntity.StartYear,
                EndYear = periodEntity.EndYear
            };
        }

            public async Task<IEnumerable<PeriodEntityListItem>> GetAllPeriodAsync()
        {
            var periods = await _dbContext.Periods
                .Select(entity => new PeriodEntityListItem
                    {
                        
                    })
                    .ToListAsync();

            return periods;
        }
    }
}

        //todo GetAll Periods
        //todo Update Period
        //todo Delete Period