using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Admin;
using GroupProject.Models.Token;
using GroupProject.Services.Admin;
using GroupProject.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ITokenService _tokenService;
        public AdminController(IAdminService adminService, ITokenService tokenService)
        {
            _adminService = adminService;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var registerResult = await _adminService.RegisterAdminAsync(model);
            if (registerResult)
                return Ok("Admin was registered");
            return BadRequest("Admin could not be registered");
        }
        [HttpPost]
        [Route("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tokenResponse = await _tokenService.GetTokenAsync(request);
            if (tokenResponse is null)
                return BadRequest("Invalid username or password");
            return Ok(tokenResponse);
        }

        [Authorize]
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetAdminById([FromRoute] int Id)
        {
            var adminDetail = await _adminService.GetAdminByIdAsync(Id);
            if (adminDetail is null)
                return NotFound();
            return Ok(adminDetail);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminUpdate request)
        {
            var currentUserClaims = HttpContext.User.Identity as ClaimsIdentity;
            var currentUserId = currentUserClaims.FindFirst("Id")?.Value;
            if (int.Parse(currentUserId) != request.Id)
                return Unauthorized("Invalid update access of current user");
            var success = await _adminService.UpdateAdminAsync(request);
            if (success)
                return Ok("Admin info updated");
            
            return BadRequest("Admin unable to be updated");
        }
        [Authorize]
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int Id)
        {
            var success = await _adminService.DeleteAdminAsync(Id);
            if (!success)
                return BadRequest("Admin could not be deleted.");
            return Ok("Admin successfully deleted");
        }
    }
}