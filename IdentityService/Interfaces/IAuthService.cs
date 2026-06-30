using IdentityService.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace IdentityService.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequests request);
    }
}
