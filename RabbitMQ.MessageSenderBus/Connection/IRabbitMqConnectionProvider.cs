using RabbitMQ.Client;

namespace RabbitMQ.MessageSenderBus.Connection;
public interface IRabbitMqConnectionProvider
{
    Task<IConnection> GetConnectionAsync();
}