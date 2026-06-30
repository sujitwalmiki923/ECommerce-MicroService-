using IdentityService.DTOs;
using IdentityService.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]  //Ihis Class contains API Endpoints
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //Post Method to Register to a user
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequests request)
        {
            var result = await _authService.RegisterAsync(request);

            return Ok(result);
        }
    }
}
