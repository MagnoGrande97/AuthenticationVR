using DragerBackendTemplate.SOLID.Models;

namespace DragerBackendTemplate.SOLID.Helpers
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string ValidateTokenAndGetEmail(string token);
    }
}