namespace RabbitMQ.API.Domain.Entities;

public class User
{
    public int Id { get; set; } = Guid.NewGuid().GetHashCode(); 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
