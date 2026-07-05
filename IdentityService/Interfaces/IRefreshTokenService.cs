using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken(); //Creates Random tokens

        Task<RefreshToken> CreateRefreshTokenAsync(User user); //Creates full entity

        Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);

        Task RevokeAsync(RefreshToken refreshToken);
    }
}
