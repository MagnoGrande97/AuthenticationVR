using DragerBackendTemplate.SOLID.DTOs;
using DragerBackendTemplate.SOLID.Models;

namespace DragerBackendTemplate.SOLID.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task<User> GetUserFromTokenAsync(string token);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string email);
        Task<bool> UpdateUserProfileAsync(DTOs.UpdateUserProfileRequest request);
    }
}