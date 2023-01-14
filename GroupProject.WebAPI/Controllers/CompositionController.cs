using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Composition;
using GroupProject.Services.Composition;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositionController : ControllerBase
    {
        private readonly ICompositionService _compositionService;
        public CompositionController(ICompositionService compositionService )
        {
            _compositionService = compositionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComposition([FromBody] CompositionCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _compositionService.CreateCompositionAsync(request))
            return Ok("Composition entry created successfully.");

            return BadRequest();
        }
            
        [HttpGet]
        public async Task<IActionResult> GetAllCompositions()
            {
                var compositions = await _compositionService.GetAllCompositionsAsync();
                return Ok(compositions);
            }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetAllCompositionsByComposerId(int Id)
            {
                var compositions = await _compositionService.GetAllCompositionsByComposerIdAsync(Id);
                return Ok(compositions);
            }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetAllCompositionsByPeriodId(int Id)
            {
                var compositions = await _compositionService.GetAllCompositionsByPeriodIdAsync(Id);
                return Ok(compositions);
            } 
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetAllCompositionsByGenreId(int Id)
            {
                var compositions = await _compositionService.GetAllCompositionsByGenreIdAsync(Id);
                return Ok(compositions);
            }   


        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetCompositionById([FromRoute] int Id)
        {
            var response = await _compositionService.GetCompositionByIdAsync(Id);
            if (response is null)
                return NotFound();
            
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateComposerById([FromBody] CompositionUpdate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _compositionService.UpdateCompositionAsync(request)
                ? Ok("Composition entry updated successfully.")
                : BadRequest("Composition entry could not be updated.");
        } 


        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteComposition([FromRoute] int Id)
        {
            return await _compositionService.DeleteCompositionAsync(Id)
                ? Ok($"Composition was deleted successfully.") 
                : BadRequest($"Composition entry could not be deleted.");
        }
    }

}
