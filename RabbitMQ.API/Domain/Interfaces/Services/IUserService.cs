using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserView>> GetAllUsersAsync();
    Task<UserView?> GetUserByIdAsync(int id);
    Task<UserView?> CreateUserAsync(CreateUserDto user);
    Task<UserView?> UpdateUserAsync(UpdateUserDto user);
    Task<bool> DeleteUserAsync(int id);
}
