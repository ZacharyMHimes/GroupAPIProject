using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.CauseOfDeath;
using GroupProject.Services.CauseOfDeath;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CauseOfDeathController : ControllerBase
    {
        private readonly ICauseOfDeathService _causeService;
        public CauseOfDeathController(ICauseOfDeathService causeService)
        {
            _causeService = causeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCause([FromBody] CauseCreate request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                if (await _causeService.CreateCauseOfDeathAsync(request))
                    return Ok("New Cause of Death added to catalog.");
                
                return BadRequest("Cause of Death could not be added to catalog.");
            }

        [HttpGet]
        public async Task<IActionResult> GetAllCauses()
            {
                var notes = await _causeService.GetAllCausesAsync();
                return Ok();
            }
        
        [HttpGet("{causeId:int}")]
        public async Task<IActionResult> GetComposerById([FromRoute] int causeId)
        {
            var detail = await _causeService.GetCauseIdAsync(causeId);
            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComposerById([FromBody] CauseModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _causeService.UpdateCauseAsync(request)
                ? Ok("Cause of Death updated successfully.")
                : BadRequest("Cause of Death entry could not be updated.");
        }

        [HttpDelete("{causeId:int}")]
        public async Task<IActionResult> DeleteCause([FromRoute] int causeId) 
        {
            return await _causeService.DeleteCauseAsync(causeId)
                ? Ok($"Cause of Death Removed from catalog.") //todo check string concatenation syntax
                : BadRequest($"Entry could not be deleted.");
        }
    }
}