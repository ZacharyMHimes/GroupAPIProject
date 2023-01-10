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
        public async Task<IActionResult> GetAllPeriods()
        {
            return Ok();
        }

        [HttpGet("{periodId:int}")]
        public async Task<IActionResult> GetPeriodById([FromRoute] int periodId)
        {
            var detail = await _periodServices.GetPeriodAsync(periodId);
            return detail is not null
            ? Ok(detail)
            : NotFound();
        }
    }
}