using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.MessageReceiverBus.Connection;
using RabbitMQ.MessageReceiverBus.Interfaces;
using System.Text;
using Microsoft.Extensions.Logging;

namespace RabbitMQ.MessageReceiverBus.Services
{
    public class RabbitMQMessageReceiver : IRabbitMQMessageReceiver
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;
        private readonly ILogger<RabbitMQMessageReceiver> _logger;

        public RabbitMQMessageReceiver(
            IRabbitMqConnectionProvider connectionProvider,
            ILogger<RabbitMQMessageReceiver> logger)
        {
            _connectionProvider = connectionProvider;
            _logger = logger;
        }

        public async Task ReceiveMessagesAsync(
            string queueName,
            Func<string, Task> onMessage,
            CancellationToken cancellationToken = default)
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

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Mensagem recebida na fila '{Queue}': {Message}", queueName, message);

                await onMessage(message);
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer,
                cancellationToken: cancellationToken
            );

            _logger.LogInformation("Consumidor aguardando mensagens na fila '{Queue}'.", queueName);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
