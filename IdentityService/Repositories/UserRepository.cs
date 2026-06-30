using IdentityService.Data;
using IdentityService.Interfaces;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
                _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.
                FirstOrDefaultAsync(x => x.Id == id);
            //return user;
        }

        public Task<User?> GetEbyEmailAsync(string email)
        {
            return _context.Users.
                FirstOrDefaultAsync(x => x.Email == email);
            //throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await 
                _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}
