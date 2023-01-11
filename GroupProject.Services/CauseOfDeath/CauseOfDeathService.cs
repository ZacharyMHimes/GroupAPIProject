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
        
        private readonly int _causeId;
        public CauseOfDeathService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    // Create
        public async Task<bool> CreateCauseOfDeathAsync(CauseCreate request)
        {
            var causeEntity = new CauseOfDeathEntity
            {
                Id = _causeId,
                CauseOfDeath = request.CauseOfDeath
            };

            _dbContext.CausesOfDeath.Add(causeEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

    // GetAll

        public async Task<IEnumerable<CauseListItem>> GetAllCausesAsync()
        {
            var causes = await _dbContext.CausesOfDeath
                .Where(entity => entity.Id ==_causeId)
                .Select(entity => new CauseListItem
                    {
                        Id = entity.Id,
                        CauseOfDeath = entity.CauseOfDeath
                    })
                    .ToListAsync();

            return causes;
        }
    // GetByID

        public async Task<CauseModel?> GetCauseIdAsync(int causeId)
        {
                var causeEntity = await _dbContext.CausesOfDeath.FirstOrDefaultAsync(e =>
                e.Id == causeId && e.Id == _causeId);
                return causeEntity is null ? null : new CauseModel
                    {
                        Id = causeEntity.Id,
                        CauseOfDeath = causeEntity.CauseOfDeath 
                    };

        }

    //todo Get List of Composers by death Id.

    // Update

        public async Task<bool> UpdateCauseAsync(CauseModel request)
        {
            var causeEntity = await _dbContext.CausesOfDeath.FindAsync(request.Id);
            if(causeEntity?.Id != _causeId)
                return false;

            causeEntity.Id = request.Id;
            causeEntity.CauseOfDeath = request.CauseOfDeath;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

    // Delete

        public async Task<bool> DeleteCauseAsync(int causeId)
        {
            var causeEntity = await _dbContext.CausesOfDeath.FindAsync(causeId);
            if(causeEntity?.Id != _causeId)
                return false;
            _dbContext.CausesOfDeath.Remove(causeEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        public Task<CauseModel> UpdateCauseAsync(int causeId)
        {
            throw new NotImplementedException();
        }
    }
}