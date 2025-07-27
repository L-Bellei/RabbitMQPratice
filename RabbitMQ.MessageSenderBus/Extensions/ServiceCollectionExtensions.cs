using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.MessageSenderBus.Configuration;
using RabbitMQ.MessageSenderBus.Connection;
using RabbitMQ.MessageSenderBus.Interfaces;
using RabbitMQ.MessageSenderBus.Services;

namespace RabbitMQ.MessageSenderBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqSenderBus(this IServiceCollection services, Action<RabbitMqSettings>? setupAction = null)
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
        services.AddTransient<IRabbitMQMessageSender, RabbitMQMessageSender>();

        return services;
    }
}
