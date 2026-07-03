using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync (RefreshToken refreshToken);

        Task<RefreshToken> GetByTokenAsync(string token);

        Task UpdateAsync(RefreshToken refreshToken);
    }
}
