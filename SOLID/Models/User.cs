using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DragerBackendTemplate.SOLID.Models
{
    public enum UserRole
    {
        User,
        Admin
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.User; // Valor por defecto
    }
}