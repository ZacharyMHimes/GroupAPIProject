using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composition;

namespace GroupProject.Services.Composition
{
    public interface ICompositionService
    {
        Task<IEnumerable<CompositionListItem>> GetAllCompositionsAsync();
        Task<bool> UpdateCompositionAsync(CompositionUpdate request);
        Task<bool> DeleteCompositionAsync(int Id);   
    }
}