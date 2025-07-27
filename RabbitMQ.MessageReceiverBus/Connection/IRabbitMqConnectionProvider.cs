using RabbitMQ.Client;

namespace RabbitMQ.MessageReceiverBus.Connection;
public interface IRabbitMqConnectionProvider : IAsyncDisposable
{
    Task<IConnection> GetConnectionAsync();
}