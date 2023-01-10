using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composer;
using GroupProject.Services.Composer;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposerController : ControllerBase
    {
        private readonly IComposerService _composerService;
        public ComposerController(IComposerService composerService)
        {
            _composerService = composerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComposer([FromBody] ComposerCreate request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                if (await _composerService.CreateComposerAsync(request))
                    return Ok("Composer added to catalog.");
                
                return BadRequest("Composer could not be added to catalog.");
            }
        
        [HttpGet]
        public async Task<IActionResult> GetAllComposers()
            {
                var notes = await _composerService.GetAllComposersAsync();
                return Ok();
            }
        
    }
}