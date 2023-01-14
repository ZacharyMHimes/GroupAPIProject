using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.CauseOfDeath;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.CauseOfDeath
{
    public class CauseOfDeathService : ICauseOfDeathService
    {
        private readonly ApplicationDbContext _dbContext;
        public CauseOfDeathService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    // Create
        public async Task<bool> CreateCauseOfDeathAsync(CauseCreate request)
        {
            var causeEntity = new CauseOfDeathEntity
            {
                CauseOfDeath = request.CauseOfDeath
            };

            _dbContext.CausesOfDeath.Add(causeEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    // Get
        public async Task<IEnumerable<CauseListItem>> GetAllCausesAsync()
        {
            var causes = await _dbContext.CausesOfDeath
                .Select(entity => new CauseListItem
                    {
                        Id = entity.Id,
                        CauseOfDeath = entity.CauseOfDeath
                    })
                    .ToListAsync();

            return causes;
        }

        public async Task<CauseModel?> GetCauseIdAsync(int causeId)
        {
                var causeEntity = await _dbContext.CausesOfDeath.FirstOrDefaultAsync(e =>e.Id == causeId);
                return causeEntity is null ? null : new CauseModel
                    {
                        Id = causeEntity.Id,
                        CauseOfDeath = causeEntity.CauseOfDeath 
                    };
        }

    // Update
        public async Task<bool> UpdateCauseAsync(CauseModel request)
        {
            var causeEntity = await _dbContext.CausesOfDeath.FindAsync(request.Id);
            causeEntity.Id = request.Id;
            causeEntity.CauseOfDeath = request.CauseOfDeath;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    // Delete
        public async Task<bool> DeleteCauseAsync(int causeId)
        {
            var causeEntity = await _dbContext.CausesOfDeath.FindAsync(causeId);
            _dbContext.CausesOfDeath.Remove(causeEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}