using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composer;
using GroupProject.Services.Composer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
                var composers = await _composerService.GetAllComposersAsync();
                return Ok(composers);
            }

        [HttpGet("ById/{Id:int}")]
        public async Task<IActionResult> GetComposerById([FromRoute] int Id)
        {
            var detail = await _composerService.GetComposerIdAsync(Id);
            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [HttpPut("UpdateComposerDetail")]
        public async Task<IActionResult> UpdateComposerById([FromBody] ComposerUpdate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _composerService.UpdateComposerAsync(request)
                ? Ok("Composer entry updated successfully.")
                : BadRequest("Composer entry could not be updated.");
        }

        [HttpPut("UpdateSexyQuotient")]
        public async Task<IActionResult> UpdateComposerSexyQuotient([FromBody] ComposerUpdateSexy sexy)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _composerService.UpdateComposerSexyQuotientAsync(sexy)
                ? Ok("Weird that you think about how sexy all these dead guys are.")
                : BadRequest("Could not update Sexy Quotient value.");
        }

        [Authorize]
        [HttpDelete("Delete/{Id:int}")]
        public async Task<IActionResult> DeleteComposer([FromRoute] int Id) 
        {
            return await _composerService.DeleteComposerAsync(Id)
                ? Ok($"Composer was deleted successfully.") 
                : BadRequest($"Composer Entry could not be deleted.");
        }
        
        [HttpGet("BySexyQuotient/{numberOfComposers:int}")]
        public async Task<IActionResult> ComposerBySexyQuotient([FromRoute] int numberOfComposers)
        {
            var composers = await _composerService.GetComposersByHotnessAsync(numberOfComposers);
                return Ok(composers);
        }
    }
}