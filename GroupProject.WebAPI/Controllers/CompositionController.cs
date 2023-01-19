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
        public CompositionController(ICompositionService compositionService)
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
        [Route("{CompositionId:int}")]
        public async Task<IActionResult> GetCompositionById([FromRoute] int CompositionId)
        {
            var response = await _compositionService.GetCompositionByIdAsync(CompositionId);
            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [Route("ByComposer/{ComposerId:int}")]
        public async Task<IActionResult> GetAllCompositionsByComposerId([FromRoute] int ComposerId)
        {
            var compositions = await _compositionService.GetAllCompositionsByComposerIdAsync(ComposerId);
            if (compositions is null)
                return NotFound("No Compositions by this Composer were found.");
            return Ok(compositions);
        }

        [HttpGet]
        [Route("ByPeriod/{PeriodId:int}")]
        public async Task<IActionResult> GetAllCompositionsByPeriodId([FromRoute] int PeriodId)
        {
            var compositions = await _compositionService.GetAllCompositionsByPeriodIdAsync(PeriodId);
            if (compositions is null)
                return NotFound("No Compositions exist within this Period.");
            return Ok(compositions);
        }
        [HttpGet]
        [Route("ByGenre/{GenreId:int}")]
        public async Task<IActionResult> GetAllCompositionsByGenreId([FromRoute] int GenreId)
        {
            var compositions = await _compositionService.GetAllCompositionsByGenreIdAsync(GenreId);
            if (compositions is null)
                return NotFound("No Compositions of this Genre were found.");
            return Ok(compositions);
        }

        [HttpPut]
        [Route("UpdateCompositionDetail/")]
        public async Task<IActionResult> UpdateCompositionById([FromBody] CompositionUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _compositionService.UpdateCompositionAsync(request)
                ? Ok("Composition entry updated successfully.")
                : BadRequest("Composition entry could not be updated.");
        }

        [HttpPut]
        [Route("UpdateCompositionDitters/")]
        public async Task<IActionResult> UpdateCompositionDitters([FromBody] CompositionUpdateDitter request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(request.DitterDorfs is 0)
                return BadRequest("DitterDorfs must be -1 or 1. Zero not a valid value.");

            return await _compositionService.UpdateCompositionDittersAsync(request)
                ? Ok()
                : BadRequest("An Error Occurred");
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
