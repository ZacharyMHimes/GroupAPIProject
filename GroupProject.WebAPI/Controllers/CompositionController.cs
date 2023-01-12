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
        
    }
}