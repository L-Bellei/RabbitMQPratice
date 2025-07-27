namespace RabbitMQ.MessageReceiverBus.Interfaces;
public interface IRabbitMQMessageReceiver
{
    Task ReceiveMessagesAsync(
                string queueName,
                Func<string, Task> onMessage,
                CancellationToken cancellationToken = default);
}
