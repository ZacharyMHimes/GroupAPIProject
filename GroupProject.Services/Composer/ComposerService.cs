using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;

namespace GroupProject.Services.Composer 
{
    public class ComposerService : IComposerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly int _composerId;

        public ComposerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateComposerAsync(ComposerCreate request)
        {
            var composerEntity = new ComposerEntity
            {
                Id = _composerId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nationality = request.Nationality,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                SexyQuotientUpVotes = request.SexyQuotientUpVotes = 0,
                SexyQuotientTotalVotes = request.SexyQuotientTotalVotes = 0,
            };
        }

        public async Task<IEnumerable<ComposerListItem>> GetAllComposersAsync()
        {
            var composers = await _dbContext.Composers
                .Where(entity => entity.Id == _composerId)
                .Select(entity => new ComposerListItem
                    {
                        // Id = entity.Id; (we don't need to deliver the Id to the model, correct?)
                        // build model input here

                    });
                    .ToListAsync();
        }

        public async Task<ComposerDetail> GetComposerIdAsync(int composerId)
        {
                var composerEntity = await _dbContext.Composers.FirstOrDefaultAsync(e =>
                e.Id == composerId && e.Id == _composerId);
                return composerEntity is null ? null : new ComposerDetail
                    {
                        // Build Composer Detail Model input here



                    };
        }

// Should Admin be able to edit sexy quotient at will?
        public async Task<bool> UpdateComposerAsync(ComposerUpdate request)
        {
            var composerEntity = await _dbContext.Composers.FindAsync(request.Id);
            if(composerEntity?.Id != _composerId)
                return false;
            composerEntity.FirstName = request.FirstName;
            composerEntity.LastName = request.LastName;
            composerEntity.Nationality = request.Nationality;
            composerEntity.BirthDate = request.BirthDate;
            composerEntity.DeathDate = request.DeathDate;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }

        public async Task<bool> DeleteNoteAsync(int composerId)
        {
            var composerEntity = await _dbContext.Composers.FindAsync(composerId);
            if(composerEntity?.Id != _composerId)
                return false;
            _dbContext.Composers.Remove(composerEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        
    }
}