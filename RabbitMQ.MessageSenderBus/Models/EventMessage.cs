using System.Text.Json;

namespace RabbitMQ.MessageSenderBus.Models;
public class EventMessage
{
    public string Type { get; set; } = string.Empty;   
    public object? Payload { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this);
}