using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Period;
using GroupProject.Services.Period;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService _periodServices;
        public PeriodController(IPeriodService periodServices)
        {
            _periodServices = periodServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePeriod([FromBody] PeriodCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _periodServices.CreatePeriodAsync(request))
            return Ok("Period created successfully.");

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeriod()
        {
            var notes = await _periodServices.GetAllPeriodAsync();
            return Ok();
        }

        [HttpGet("{periodId:int}")]
        public async Task<IActionResult> GetPeriodById([FromRoute] int periodId)
        {
            var detail = await _periodServices.GetPeriodIdAsync(periodId);
            return detail is not null
            ? Ok(detail)
            : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePeriodById([FromBody] PeriodUpdate request)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            return await _periodServices.UpdatePeriodAsync(request)
            ? Ok("Period entry updated successfully.")
            : BadRequest("Period entry could not be updated");
        }

        [HttpDelete("{periodId:int}")]
        public async Task<IActionResult> DeletePeriod([FromRoute] int periodId)
        {
            return await _periodServices.DeletePeriodAsync(periodId)
            ? Ok($"Period entry deleted successfully.")
            : BadRequest($"Period entry could not be deleted.");
        }
    }
}