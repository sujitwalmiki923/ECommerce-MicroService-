using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string password);
    }
}