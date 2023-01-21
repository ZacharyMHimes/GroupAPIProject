using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composer;
using Microsoft.EntityFrameworkCore;
using GroupProject.Services.CauseOfDeath;
using GroupProject.Models.CauseOfDeath;

namespace GroupProject.Services.Composer 
{
    public class ComposerService : IComposerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICauseOfDeathService _cService;
        public ComposerService(ApplicationDbContext dbContext, ICauseOfDeathService cService)
        {
            _dbContext = dbContext;
            _cService = cService;
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
                SexyQuotientUpVotes = request.SexyQuotientUpVotes,
                SexyQuotientTotalVotes = request.SexyQuotientTotalVotes
            };

            //Save the New Composer to the Database
            _dbContext.Composers.Add(composerEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            //If the User didn't enter a cause of Death, return true
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
                        //if nothing is found, create a new cause of death
                        var newCauseEntity = new CauseOfDeathEntity
                        {
                            CauseOfDeath = request.CauseOfDeath
                        };
                        //link the new composer entity with the new cause of death entity
                        composerEntity.CauseOfDeath = newCauseEntity;
                        //add to _dbContext
                        _dbContext.CausesOfDeath.Add(newCauseEntity);
                        //Save to the Database and return true
                        numberOfChanges = await _dbContext.SaveChangesAsync();
                        return true;
                        // await _cService.CreateCauseOfDeathAsync(new CauseCreate
                        // {
                        //         request.CauseOfDeath;
                        // });

                    }
                    else 
                    {
                        composerEntity.CauseOfDeath = searchedCause;
                        return await _dbContext.SaveChangesAsync() == 1;
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
                        CauseOfDeath = composerEntity.CauseOfDeath?.CauseOfDeath
                        
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
        
        public async Task<bool> UpdateComposerAsync(ComposerUpdate request)
        {
            var composerEntity = await _dbContext.Composers
                                .Include(entity => entity.CauseOfDeath)
                                .FirstOrDefaultAsync(entity => entity.Id == request.Id);
            if (composerEntity is null)
                return false;
            composerEntity.FirstName = request.FirstName;
            composerEntity.LastName = request.LastName;
            composerEntity.Nationality = request.Nationality;
            composerEntity.BirthDate = request.BirthDate;
            composerEntity.DeathDate = request.DeathDate;
            composerEntity.SexyQuotientUpVotes = request.SexyQuotientUpVotes;
            composerEntity.SexyQuotientTotalVotes = request.SexyQuotientTotalVotes;
            composerEntity.CauseOfDeath = (request.CauseOfDeath is not null)
                ? await _dbContext.CausesOfDeath.FirstOrDefaultAsync(entity => entity.CauseOfDeath.ToLower() == request.CauseOfDeath.ToLower())
                : null;
            
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> UpdateComposerSexyQuotientAsync(ComposerUpdateSexy sexy)
        {
            var composerEntity = await _dbContext.Composers.FindAsync(sexy.Id);
            if (composerEntity is null)
                return false;
            
            if (sexy.IsSexy)
            {
                composerEntity.SexyQuotientUpVotes += 1;
            }
                
                composerEntity.SexyQuotientTotalVotes += 1;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteComposerAsync(int Id)
        {
            var composerEntity = await _dbContext.Composers
                    .Include(composer => composer.Compositions)
                    .FirstOrDefaultAsync(composer => composer.Id == Id);
            if (composerEntity is null)
                return false;
            _dbContext.Composers.Remove(composerEntity);
            return await _dbContext.SaveChangesAsync() > 0;
        }      
    }
}