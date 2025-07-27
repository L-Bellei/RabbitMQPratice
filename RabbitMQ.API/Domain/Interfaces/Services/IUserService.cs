using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserView>> GetAllUsersAsync();
    Task<UserView?> GetUserByIdAsync(int id);
    Task<UserView?> CreateUserAsync(UserDto user);
    Task<UserView?> UpdateUserAsync(UserDto user);
    Task<bool> DeleteUserAsync(int id);
}
