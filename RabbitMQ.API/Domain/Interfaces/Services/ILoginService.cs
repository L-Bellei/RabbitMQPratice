using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<AuthResultDto?> AuthenticateAsync(UserLoginDto dto);
}
