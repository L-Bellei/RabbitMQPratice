namespace RabbitMQ.API.Domain.Dtos;

public class EnterpriseDto
{
    public int Id { get; set; } 
    public string? Name { get; set; } 
    public string? Description { get; set; }  
    public DateTime CreatedAt { get; set; }  
    public DateTime UpdatedAt { get; set; }  
}
