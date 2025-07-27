namespace RabbitMQ.MessageReceiverBus.Configuration;

public record RabbitMqSettings(string HostName, string UserName, string Password, int Port);