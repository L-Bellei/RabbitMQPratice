namespace RabbitMQ.MessageBus;
public class BaseMessage
{
    public long Id { get; set; }
    public DateTime MessageCreatedAt { get; set; }
}
