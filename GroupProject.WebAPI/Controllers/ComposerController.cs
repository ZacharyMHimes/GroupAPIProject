using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Services.Composer;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    public class ComposerController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        private readonly IComposerService _composerService;
        public ComposerController(IComposerService composerService)
        {
            _composerService = composerService;
        }
    }
}