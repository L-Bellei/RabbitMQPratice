using RabbitMQ.Client;
using RabbitMQ.MessageSenderBus.Connection;
using RabbitMQ.MessageSenderBus.Interfaces;
using RabbitMQ.MessageSenderBus.Models;
using System.Text;

namespace RabbitMQ.MessageSenderBus.Services;
public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly IRabbitMqConnectionProvider _connectionProvider;

    public RabbitMQMessageSender(IRabbitMqConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task SendMessageAsync(EventMessage message, string queueName, CancellationToken cancellationToken = default)
    {
        var connection = await _connectionProvider.GetConnectionAsync();
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken
        );
  
        var body = Encoding.UTF8.GetBytes(message.ToJson());

        await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body, cancellationToken: cancellationToken);
    }
}