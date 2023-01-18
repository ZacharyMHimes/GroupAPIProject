using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composer;
using Microsoft.EntityFrameworkCore;
using GroupProject.Services.CauseOfDeath;

namespace GroupProject.Services.Composer 
{
    public class ComposerService : IComposerService
    {
        private readonly ApplicationDbContext _dbContext;
        public ComposerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> CreateComposerAsync(ComposerCreate request)
        {
            //create New Composer Entity
            var composerEntity = new ComposerEntity
            {
                //Assign Values from Model
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nationality = request.Nationality,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                SexyQuotientUpVotes = request.SexyQuotientUpVotes = 0,
                SexyQuotientTotalVotes = request.SexyQuotientTotalVotes = 0,
                //Check to see if a matching cause of death is in the Database.
                //CauseOfDeath = await _dbContext.CausesOfDeath.FirstOrDefaultAsync(request.CauseOfDeath)
            };

            //Save the New Composer to the Database
            _dbContext.Composers.Add(composerEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            //If the User didn't enter a cause of Death, return true.
            if(request.CauseOfDeath is null || request.CauseOfDeath.Length == 0)
            {
                return true;
            }
            //After Saving the entity to the Database
            if(numberOfChanges > 0)
                {
                    //search _dbContext (or database) for a Cause of Death matching request.CauseOfDeath
                    var searchedCause = await _dbContext.CausesOfDeath.FirstOrDefaultAsync(c => c.CauseOfDeath == request.CauseOfDeath);
                    if (searchedCause is null)
                    {
                        //if nothing is found, create a new cause of death and add to _dbContext.
                        _dbContext.CausesOfDeath.Add(new CauseOfDeathEntity
                        {
                            CauseOfDeath = request.CauseOfDeath
                        });
                        //Save to the Database and return true
                        numberOfChanges = await _dbContext.SaveChangesAsync();
                        return true;
                    }
                }

            return false;            
        }

        public async Task<IEnumerable<ComposerListItem>> GetAllComposersAsync()
        {
            var composers = await _dbContext.Composers
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
                var composerEntity = await _dbContext.Composers
                                    .Include(entity => entity.CauseOfDeath)
                                    .FirstOrDefaultAsync(e =>e.Id == composerId);
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
                        CauseOfDeath = composerEntity.CauseOfDeath.CauseOfDeath
                        
                    };
        }

// Get Sexiest Composers
        public async Task<IEnumerable<ComposerSexyListItem>> GetComposersByHotnessAsync(int numberOfComposers)
        {
            var composers = await _dbContext.Composers
                            .OrderByDescending(composer => composer.SexyQuotientUpVotes/composer.SexyQuotientTotalVotes)
                            .Take(numberOfComposers)
                            .Select(entity => new ComposerSexyListItem
                                {
                                    Id = entity.Id,
                                    FirstName = entity.FirstName,
                                    LastName = entity.LastName,
                                    SexyQuotient = (float) entity.SexyQuotientUpVotes/(float) entity.SexyQuotientTotalVotes
                                })
                            .ToListAsync();
                    
            return composers;
        }
// Should Admin be able to edit sexy quotient at will?
//todo - Add Update Composer SexyQuotient Async Method
        public async Task<bool> UpdateComposerAsync(ComposerUpdate request)
        {
            var composerEntity = await _dbContext.Composers
                                .Include(entity => entity.CauseOfDeath)
                                .FirstOrDefaultAsync(entity => entity.Id == request.Id);
            composerEntity.FirstName = request.FirstName;
            composerEntity.LastName = request.LastName;
            composerEntity.Nationality = request.Nationality;
            composerEntity.BirthDate = request.BirthDate;
            composerEntity.DeathDate = request.DeathDate;
            composerEntity.SexyQuotientUpVotes = request.SexyQuotientUpVotes;
            composerEntity.SexyQuotientTotalVotes = request.SexyQuotientTotalVotes;
            composerEntity.CauseOfDeath = await _dbContext.CausesOfDeath.FirstOrDefaultAsync(entity => entity.CauseOfDeath.ToLower() == request.CauseOfDeath.ToLower());

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }

        public async Task<bool> DeleteComposerAsync(int Id)
        {
            var composerEntity = await _dbContext.Composers.FindAsync(Id);
            _dbContext.Composers.Remove(composerEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        
    }
}