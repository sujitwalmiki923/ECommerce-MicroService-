using IdentityService.DTOs;
using IdentityService.Exceptions;
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
        private readonly ILogger<AuthService> _logger;
        private readonly IRefreshTokenService _refreshTokenService;
        public AuthService(IUserRepository userRepository , IPasswordService passwordService , IJwtService jwtService ,ILogger<AuthService> logger , IRefreshTokenService refreshTokenService )
        {
             _userRepository = userRepository; 
            _passwordService = passwordService;
            _jwtService = jwtService;
            _logger = logger;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation("Login attempt for {Email}", request.Email);
            //Step 1: Find USer
            var user = await _userRepository.GetEbyEmailAsync(request.Email);

            if(user == null) 
            {
                _logger.LogWarning("Login failed. User {Email} not found.",request.Email);
                throw new UnauthorizedException("Invalid Email or Password");
            }

            //Step 2: Verify Password
            bool isPasswordValid = _passwordService.VerifyPassword(user, request.Password);

            if (!isPasswordValid) 
            {
                _logger.LogWarning( "Invalid password for {Email}",request.Email);

                throw new UnauthorizedException("Invalid email or password.");
                throw new UnauthorizedException("Invalid email or password");
            }

            //Step 3: Generate JWT Token
            var accesstoken = _jwtService.GenerateToken(user);

            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user);

            _logger.LogInformation( "User {UserId} logged in successfully.", user.Id);
            return new LoginResponse 
            {
                AccessToken = accesstoken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };

            // throw new NotImplementedException();
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequests request)
        {
            _logger.LogInformation("Register request received for {Email} ", request.Email);

            var existingUser = await _userRepository.GetEbyEmailAsync(request.Email);

            if(existingUser != null)
            {
                _logger.LogWarning("Registration failed. Email {Email} already exists.", request.Email);

                // throw new Exception("Email already exists");
                throw new ConflictException("Email already exists");
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

            _logger.LogInformation(
    "User {UserId} registered successfully.",
    user.Id);

            return new RegisterResponse 
            {
                UserId = user.Id,
                Message = "User registered successfully"
            };


        }
    }
}
