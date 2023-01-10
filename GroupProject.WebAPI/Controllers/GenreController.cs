using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Genre;
using GroupProject.Services.Genre;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreRegister request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                if (await _genreService.CreateGenreAsync(request))
                    return Ok("Genre added to catalog.");
                
                return BadRequest("Genre could not be added to catalog.");
            }

        [HttpPut]
        public async Task<IActionResult> UpdateGenreById([FromBody] GenreUpdate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _genreService.UpdateGenreAsync(request)
                ? Ok("Genre entry updated successfully.")
                : BadRequest("Genre entry could not be updated.");
        }

        [HttpDelete("{noteId:int}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int genreId)
        {
            return await _genreService.DeleteGenreAsync(genreId)
                ? Ok($"Genre Entry {genreId} was deleted successfully.")
                : BadRequest($"Genre Entry {genreId} could not be deleted.");
        }
    }
}