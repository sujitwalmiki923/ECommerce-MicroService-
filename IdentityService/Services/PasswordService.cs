using IdentityService.Interfaces;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password)
        {
            try 
            {
                var result = _passwordHasher.VerifyHashedPassword(
                 user,
                 user.PasswordHash,
                 password
                );
                return result == PasswordVerificationResult.Success;
            }
            catch (FormatException)
            {
                return false;
            }
            
        }

    }
}
