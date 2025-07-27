using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<UserView?> AuthenticateAsync(UserLoginDto dto);
}
