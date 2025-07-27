using RabbitMQ.Client;
using RabbitMQ.MessageReceiverBus.Configuration;

namespace RabbitMQ.MessageReceiverBus.Connection
{
    public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider, IAsyncDisposable
    {
        private readonly RabbitMqSettings _settings;
        private IConnection? _connection;

        public RabbitMqConnectionProvider(RabbitMqSettings settings)
        {
            _settings = settings;
        }

        public async Task<IConnection> GetConnectionAsync()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                var factory = new ConnectionFactory
                {
                    HostName = _settings.HostName,
                    UserName = _settings.UserName,
                    Password = _settings.Password,
                    Port = _settings.Port
                };

                _connection = await factory.CreateConnectionAsync();
            }
            return _connection;
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}
