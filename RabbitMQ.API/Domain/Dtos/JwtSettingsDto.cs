namespace RabbitMQ.API.Domain.Dtos;

public class JwtSettingsDto
{
    public string Issuer { get;set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}
