namespace RabbitMQ.API.Domain.Services;

public partial class UserService
{
    private static string HashPassword(string password) => 
        BCrypt.Net.BCrypt.HashPassword(password);
}
