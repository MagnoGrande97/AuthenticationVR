using DragerBackendTemplate.SOLID.Models;

namespace DragerBackendTemplate.SOLID.DTOs
{
    public class UpdateUserProfileRequest
    {
        public string Email { get; set; } // Identificador
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        // NUEVO (opcional)
        public UserRole? Role { get; set; }
    }
}