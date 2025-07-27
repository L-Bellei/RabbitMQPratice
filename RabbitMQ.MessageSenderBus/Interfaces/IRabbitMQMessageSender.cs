using RabbitMQ.MessageSenderBus.Models;

namespace RabbitMQ.MessageSenderBus.Interfaces;
public interface IRabbitMQMessageSender
{
    Task SendMessageAsync(EventMessage message, string queueName, CancellationToken cancellationToken = default);
}
