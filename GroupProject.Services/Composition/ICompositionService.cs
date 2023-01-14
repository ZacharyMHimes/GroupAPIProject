using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composition;

namespace GroupProject.Services.Composition
{
    public interface ICompositionService
    {
        Task<bool> CreateCompositionAsync(CompositionCreate compositionEntity);
        Task<CompositionDetail?> GetCompositionByIdAsync(int Id);
        Task<IEnumerable<CompositionListItem>> GetAllCompositionsAsync();
        Task<IEnumerable<CompositionListItem>> GetAllCompositionsByComposerIdAsync(int composerId);
        Task<bool> UpdateCompositionAsync(CompositionUpdate request);
        Task<bool> DeleteCompositionAsync(int Id);   
    }
}