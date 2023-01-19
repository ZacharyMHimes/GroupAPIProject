using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Admin;
using GroupProject.Services.Admin;
using GroupProject.Services.Token;
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
    }
}