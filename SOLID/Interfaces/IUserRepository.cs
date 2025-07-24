using DragerBackendTemplate.SOLID.DTOs;
using DragerBackendTemplate.SOLID.Models;

namespace DragerBackendTemplate.SOLID.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task CreateAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<bool> DeleteByEmailAsync(string email);
        Task<bool> UpdateProfileAsync(UpdateUserProfileRequest request);
    }
}