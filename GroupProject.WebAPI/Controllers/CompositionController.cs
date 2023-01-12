using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("{id}")]
        public async Task<IActionResult> GetCompositionById([FromRoute] int Id)
        {
            var response = await _compositionService.GetCompositionByIdAsync(Id);
            if (response is null)
                return NotFound();
            
            return Ok(response);
        }
    }
}