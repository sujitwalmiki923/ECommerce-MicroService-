using IdentityService.DTOs;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace IdentityService.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequests request);

        Task<LoginResponse> LoginAsync(DTOs.LoginRequest request);

        //Generate Refresh Token
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}

