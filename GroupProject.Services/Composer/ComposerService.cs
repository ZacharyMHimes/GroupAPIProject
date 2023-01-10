using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composer;
using Microsoft.EntityFrameworkCore;

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
                Id = _composerId,  //* how is the Key Id generated? From the front end?
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nationality = request.Nationality,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                SexyQuotientUpVotes = request.SexyQuotientUpVotes = 0,
                SexyQuotientTotalVotes = request.SexyQuotientTotalVotes = 0,
            };

            _dbContext.Composers.Add(composerEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ComposerListItem>> GetAllComposersAsync()
        {
            var composers = await _dbContext.Composers
                .Where(entity => entity.Id == _composerId)
                .Select(entity => new ComposerListItem
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    })
                    .ToListAsync();

            return composers;
        }

        public async Task<ComposerDetail?> GetComposerIdAsync(int composerId)
        {
                var composerEntity = await _dbContext.Composers.FirstOrDefaultAsync(e =>
                e.Id == composerId && e.Id == _composerId);
                return composerEntity is null ? null : new ComposerDetail
                    {
                        Id = composerEntity.Id,
                        FirstName = composerEntity.FirstName,
                        LastName = composerEntity.LastName,
                        Nationality = composerEntity.Nationality,
                        BirthDate = composerEntity.BirthDate,
                        DeathDate = composerEntity.DeathDate,
                        SexyQuotientUpVotes = composerEntity.SexyQuotientUpVotes,
                        SexyQuotientTotalVotes = composerEntity.SexyQuotientTotalVotes,
                        
                        //*Maybe some fancy magic to display cause of death?
                    };
        }

// Should Admin be able to edit sexy quotient at will?
//todo - Add Update Composer SexyQuotient Async Method
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

        public async Task<bool> DeleteComposerAsync(int composerId)
        {
            var composerEntity = await _dbContext.Composers.FindAsync(composerId);
            if(composerEntity?.Id != _composerId)
                return false;
            _dbContext.Composers.Remove(composerEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        
    }
}