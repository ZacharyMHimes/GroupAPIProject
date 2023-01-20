using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Instrument;
using GroupProject.Services.Instrument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;
        public InstrumentController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateInstrument([FromBody] InstrumentCreate request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                if (await _instrumentService.CreateInstrumentAsync(request))
                    return Ok("Instrument added to catalog.");
                
                return BadRequest("Instrument could not be added to catalog.");
            }

        [HttpGet]
        public async Task<IActionResult> GetAllInstruments()
            {
                var instruments = await _instrumentService.GetAllInstrumentsAsync();
                return Ok(instruments);
            }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetInstrumentById([FromRoute] int Id)
        {
            var detail = await _instrumentService.GetInstrumentIdAsync(Id);
            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstrumentById([FromBody] InstrumentUpdate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _instrumentService.UpdateInstrumentAsync(request)
                ? Ok("Instrument entry updated successfully.")
                : BadRequest("Instrument entry could not be updated.");
        }

        [Authorize]
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteInstrument([FromRoute] int Id)
        {
            return await _instrumentService.DeleteInstrumentAsync(Id)
                ? Ok($"Instrument Entry was deleted successfully.")
                : BadRequest($"Instrument Entry could not be deleted.");
        }
    }
}