using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composition;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Composition
{
    public class CompositionService : ICompositionService
    {
        private readonly ApplicationDbContext _dbContext;
        public CompositionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<IEnumerable<CompositionListItem>> GetAllCompositionsAsync()
        {
            var compositions = await _dbContext.Compositions
                .Select(entity => new CompositionListItem
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        ComposerId = entity.Composer.Id
                    })
                    .ToListAsync();

            return compositions;
        }

    }
}