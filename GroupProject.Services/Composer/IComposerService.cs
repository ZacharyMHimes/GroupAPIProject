using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composer;

namespace GroupProject.Services.Composer
{
    public interface IComposerService
    {
    Task<bool> CreateComposerAsync(ComposerCreate request);
    Task<IEnumerable<ComposerListItem>> GetAllComposersAsync();
    Task<ComposerDetail?> GetComposerIdAsync(int composerId);
    Task<IEnumerable<ComposerSexyListItem>> GetComposersByHotnessAsync(int numberOfComposers);
    Task<bool> UpdateComposerAsync(ComposerUpdate request);
    Task<bool> UpdateComposerSexyQuotientAsync(ComposerUpdateSexy sexy);
    Task<bool> DeleteComposerAsync(int composerId);

    }
}