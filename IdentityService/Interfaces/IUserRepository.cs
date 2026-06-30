using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetEbyEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);

        Task AddAsync(User user);

        Task SaveChangesAsync();

    }
}
