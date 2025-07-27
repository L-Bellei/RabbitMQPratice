using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Dtos;

public class AuthResultDto
{
    public UserView? User { get; set; }
    public string? Token { get; set; }
}
