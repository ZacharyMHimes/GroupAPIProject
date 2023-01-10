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

        [HttpGet("{noteId:int}")]
        public async Task<IActionResult> GetComposerById([FromRoute] int composerId)
        {
            var detail = await _composerService.GetComposerIdAsync(composerId);
            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComposerById([FromBody] ComposerUpdate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _composerService.UpdateComposerAsync(request)
                ? Ok("Composer entry updated successfully.")
                : BadRequest("Composer entry could not be updated.");
        }
        
        [HttpDelete("{noteId:int}")]
        public async Task<IActionResult> DeleteComposer([FromRoute] int composerId) //*it'd be cool to pass in First and Last name so return string could say their name instead of arbitrary ID.
        {
            return await _composerService.DeleteComposerAsync(composerId)
                ? Ok($"Composer Entry {composerId} was deleted successfully.")
                : BadRequest($"Composer Entry {composerId} could not be deleted.");
        }
        
    }
}