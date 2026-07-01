using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet("profile")]
        [Authorize]
        public IActionResult Index()
        {
            return Ok(new
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value,

                Email = User.FindFirst(ClaimTypes.Email)?.Value,

                Role = User.FindFirst(ClaimTypes.Role)?.Value




            });
        }
    }
}
