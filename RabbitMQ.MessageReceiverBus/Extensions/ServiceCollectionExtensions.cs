using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.MessageReceiverBus.Configuration;
using RabbitMQ.MessageReceiverBus.Connection;
using RabbitMQ.MessageReceiverBus.Interfaces;
using RabbitMQ.MessageReceiverBus.Services;

namespace RabbitMQ.MessageReceiverBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqReceiverBus(this IServiceCollection services, Action<RabbitMqSettings>? setupAction = null)
    {
        var settings = new RabbitMqSettings(
            HostName: Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME") ?? "localhost",
            UserName: Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
            Password: Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
            Port: int.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"), out var port) ? port : 5672
        );

        setupAction?.Invoke(settings);
        
        services.AddSingleton(settings);
        services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>();
        services.AddTransient<IRabbitMQMessageReceiver, RabbitMQMessageReceiver>();

        return services;
    }
}
