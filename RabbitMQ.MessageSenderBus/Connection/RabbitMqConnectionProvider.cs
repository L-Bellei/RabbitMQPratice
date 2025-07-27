using RabbitMQ.Client;
using RabbitMQ.MessageSenderBus.Configuration;

namespace RabbitMQ.MessageSenderBus.Connection;
public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider, IDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;

    public RabbitMqConnectionProvider(RabbitMqSettings settings)
    {
        _factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            UserName = settings.UserName,
            Password = settings.Password,
            Port = settings.Port
        };
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection == null || !_connection.IsOpen)
            _connection = await _factory.CreateConnectionAsync();
        return _connection;
    }

    public void Dispose() => _connection?.Dispose();
}