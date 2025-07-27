using RabbitMQ.API.Domain.Entities;

namespace RabbitMQ.API.Domain.Interfaces.Repos;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByNameAsync(string name);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> AddUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}
