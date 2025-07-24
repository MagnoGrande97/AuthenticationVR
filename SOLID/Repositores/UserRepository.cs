using DragerBackendTemplate.SOLID.DTOs;
using DragerBackendTemplate.SOLID.Interfaces;
using DragerBackendTemplate.SOLID.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DragerBackendTemplate.SOLID.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoDbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _users = database.GetCollection<User>(dbSettings.Value.UsersCollectionName);
        }

        public async Task<User> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        public async Task<List<User>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var result = await _users.DeleteOneAsync(u => u.Email == email);
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateProfileAsync(UpdateUserProfileRequest request)
        {
            var update = Builders<User>.Update
                .Set(u => u.FirstName, request.FirstName)
                .Set(u => u.LastName, request.LastName)
                .Set(u => u.PhoneNumber, request.PhoneNumber);

            // AÑADIR si el rol viene en el request
            if (request.Role.HasValue)
            {
                update = update.Set(u => u.Role, request.Role.Value);
            }

            var result = await _users.UpdateOneAsync(u => u.Email == request.Email, update);
            return result.ModifiedCount > 0;
        }
    }
}