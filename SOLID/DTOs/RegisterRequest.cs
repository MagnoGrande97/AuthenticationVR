using DragerBackendTemplate.SOLID.Models; // Asegúrate de importar UserRole

namespace DragerBackendTemplate.SOLID.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; } = UserRole.User; // Nuevo campo
    }
}