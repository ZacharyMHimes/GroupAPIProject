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

        [HttpGet]
        public async Task<IActionResult> GetAllCompositions()
            {
                var notes = await _compositionService.GetAllCompositionsAsync();
                return Ok();
            }
            
        [HttpGet]
        [Route("{id}")]
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
                ? Ok("Composer entry updated successfully.")
                : BadRequest("Composer entry could not be updated.");
        } 


        [HttpDelete("{Id:int} {firstName:string} {lastName:string}")]
        public async Task<IActionResult> DeleteComposer([FromRoute] int Id, string Title)
        {
            return await _compositionService.DeleteCompositionAsync(Id)
                ? Ok($"Composition: {Title} was deleted successfully.") 
                : BadRequest($"Composition entry: {Title} could not be deleted.");
        }
     }

}