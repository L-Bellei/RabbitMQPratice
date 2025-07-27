namespace RabbitMQ.API.Domain.Dtos;

public class JwtSettingsDto(string issuer, string audience, string key)
{
    public string Issuer { get; set; } = issuer;
    public string Audience { get; set; } = audience;
    public string Key { get; set; } = key;
}
