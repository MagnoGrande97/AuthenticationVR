using DragerBackendTemplate.SOLID.DTOs;
using DragerBackendTemplate.SOLID.Helpers;
using DragerBackendTemplate.SOLID.Interfaces;
using DragerBackendTemplate.SOLID.Models;
using Microsoft.AspNetCore.Identity;

namespace DragerBackendTemplate.SOLID.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null) throw new Exception("User already exists");

            var newUser = new User
            {
                Email = request.Email,
                Role = request.Role // nuevo
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);
            await _userRepository.CreateAsync(newUser);

            return _jwtTokenGenerator.GenerateToken(newUser);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null) throw new Exception("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid credentials");

            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task<User> GetUserFromTokenAsync(string token)
        {
            var email = _jwtTokenGenerator.ValidateTokenAndGetEmail(token);
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            return await _userRepository.DeleteByEmailAsync(email);
        }

        public async Task<bool> UpdateUserProfileAsync(DTOs.UpdateUserProfileRequest request)
        {
            return await _userRepository.UpdateProfileAsync(request);
        }
    }
}