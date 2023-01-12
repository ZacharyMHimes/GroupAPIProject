using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Composition;

namespace GroupProject.Services.Composition
{
    public class CompositionService : ICompositionService
    {
        private readonly ApplicationDbContext _dbContext;
        public CompositionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public async Task<bool> UpdateCompositionAsync(CompositionUpdate request)
        {
            var compositionEntity = await _dbContext.Compositions.FindAsync(request.Id);
            compositionEntity.Id = request.Id;
            compositionEntity.Title = request.Title;
            compositionEntity.OpusNumber = request.OpusNumber;
            compositionEntity.TotalViews = request.TotalViews;
            compositionEntity.DitterDorfs = request.DitterDorfs;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }
        public async Task<bool> DeleteCompositionAsync(int Id)
        {
            var compositionEntity = await _dbContext.Composers.FindAsync(Id);
            _dbContext.Composers.Remove(compositionEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}