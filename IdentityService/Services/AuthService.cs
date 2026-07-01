using IdentityService.DTOs;
using IdentityService.Interfaces;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository , IPasswordService passwordService , IJwtService jwtService )
        {
             _userRepository = userRepository; 
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            //Step 1: Find USer
            var user = await _userRepository.GetEbyEmailAsync(request.Email);

            if(user == null) 
            {
                throw new Exception("Invalid Email or Password");
            }

            //Step 2: Verify Password
            bool isPasswordValid = _passwordService.VerifyPassword(user, request.Password);

            if (!isPasswordValid) 
            {
                throw new Exception("Invalid email or password");
            }

            //Step 3: Generate JWT Token
            var token = _jwtService.GenerateToken(user);

            return new LoginResponse 
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };

            // throw new NotImplementedException();
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
               
                Role = "User"

            };

            user.PasswordHash = _passwordService.HashPassword(user, request.Password);

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
