using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.Services.Composer
{
    public interface IComposerService
    {
    Task<bool> CreateComposerAsync(ComposerCreate request);
    
    }
}