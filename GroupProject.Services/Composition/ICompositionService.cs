using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composition;

namespace GroupProject.Services.Composition
{
    public interface ICompositionService
    {
        public Task<CompositionDetail?> GetCompositionByIdAsync(int Id);
    }
}