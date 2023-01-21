using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Instrumentation;
using GroupProject.Services.Instrumentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentationController : ControllerBase
    {
        private readonly IInstrumentationService _instrumentationService;
        public InstrumentationController(IInstrumentationService instrumentationService)
        {
            _instrumentationService = instrumentationService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateInstrumentation([FromBody] InstrumentationCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _instrumentationService.CreateInstrumentationAsync(request))
            return Ok("Instrumentation entry created successfully.");

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInstrumentation()
        {
            var instrumentations = await _instrumentationService.GetAllInstrumentationAsync();
            return Ok(instrumentations);
        }

        [HttpGet("{instrumentationId:int}")]
        public async Task<IActionResult> GetInstrumentationById([FromRoute] int instrumentationId)
        {
            var detail = await _instrumentationService.GetInstrumentationIdAsync(instrumentationId);
            return detail is not null
            ? Ok(detail)
            : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstrumentationById([FromBody] InstrumentationUpdate request)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            return await _instrumentationService.UpdateInstrumentationAsync(request)
            ? Ok($"Instrumentation entry updated successfully.")
            : BadRequest("Instrumentation entry could not be updated successfully.");
        }
        
        [Authorize]
        [HttpDelete("{instrumentationId:int}")]
        public async Task<IActionResult> DeleteInstrumentation([FromRoute] int instrumentationId)
        {
            return await _instrumentationService.DeleteInstrumentationAsync(instrumentationId)
            ? Ok($"Instrumentation {instrumentationId} entry was deleted successfully.")
            : BadRequest($"Instrumentation entry {instrumentationId} could not be deleted.");
        }
    }
}