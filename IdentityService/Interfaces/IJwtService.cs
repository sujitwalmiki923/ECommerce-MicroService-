using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
