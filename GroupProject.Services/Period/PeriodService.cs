using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Period;
using GroupProject.Services.Period;

namespace GroupProject.Services.Period

public class PeriodService : IPeriodService
    {
    private readonly ApplicationDbContext _context;
    private ApplicationDbContext DbContext;

    public PeriodService(ApplicationDbContext context)
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

        public async Task<bool> UpdatePeriodAsync(PeriodUpdate request)
        {
            var periodEntity = await _context.Periods.FindAsync(request.Id);
            if(periodEntity?.Id != _periodId)
                return false;
            periodEntity.PeriodName = request.PeriodName;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

            public async Task<bool> DeletePeriodAsync(int Id)
        {
            var periodEntity = await _context.Periods.FindAsync(Id);
            if (periodEntity is null)
                return false;
            _context.Periods.Remove(periodEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

            public async Task<PeriodDetail?> GetPeriodIdAsync(int periodId)
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

            public async Task<IEnumerable<PeriodEntityListItem>> GetAllPeriodAsync()
        {
            var periods = await _dbContext.Periods
                .Where(entity => entity.Id == _periodId)
                .Select(entity => new PeriodEntityListItem
                    {
                        
                    })
                    .ToListAsync();

            return periods;
        }
    }

        //todo GetAll Periods
        //todo Update Period
        //todo Delete Period