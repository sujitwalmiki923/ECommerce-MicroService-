using IdentityService.Common;
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

            // return Ok(result);

            //Production ready Generic Response

            return Ok(
                ApiResponse<RegisterResponse>.SuccessResponse(
                  result,
                  "Registration  Successful")
                );
        }

        //Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOs.LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            //return Ok(result);

            return Ok(
                ApiResponse<LoginResponse>.SuccessResponse(
                    result,
                    "Login Successful"
                    ));
        }

        //RefreshToken
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var response = 
                await _authService.RefreshTokenAsync(request);

            return Ok(
                ApiResponse<RefreshTokenResponse>.SuccessResponse(
                    response,
                    "Token Refreshed Successfully"));

        }
    }
}
