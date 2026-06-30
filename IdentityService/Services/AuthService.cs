using IdentityService.DTOs;
using IdentityService.Interfaces;
using IdentityService.Models;

namespace IdentityService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
             _userRepository = userRepository;   
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequests request)
        {
            var existingUser = await _userRepository.GetEbyEmailAsync(request.Email);

            if(existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,

                //Temporary 
                PasswordHash = request.Password,
                Role = "User"

            };

            await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();

            return new RegisterResponse 
            {
                UserId = user.Id,
                Message = "User registered successfully"
            };
        }
    }
}
