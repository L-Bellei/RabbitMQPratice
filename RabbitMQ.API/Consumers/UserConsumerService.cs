using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.MessageReceiverBus.Interfaces;
using RabbitMQ.MessageSenderBus.Models;
using System.Text.Json;

namespace RabbitMQ.API.Consumers;
public class UserConsumerService : BackgroundService
{
    private readonly IRabbitMQMessageReceiver _receiver;
    private readonly IServiceScopeFactory _scopeFactory;

    public UserConsumerService(
        IRabbitMQMessageReceiver receiver,
        IServiceScopeFactory scopeFactory)
    {
        _receiver = receiver;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _receiver.ReceiveMessagesAsync("user_events", async msg =>
        {
            using var scope = _scopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var eventMessage = JsonSerializer.Deserialize<EventMessage>(msg);

            if (eventMessage == null) return;

            switch (eventMessage.Type)
            {
                case "UserCreated":
                    {
                        var userDto = JsonSerializer.Deserialize<CreateUserDto>(eventMessage.Payload?.ToString() ?? "");
                        if (userDto != null)
                            await userService.CreateUserAsync(userDto);
                    }
                    break;

                case "UserUpdated":
                    {
                        var userDto = JsonSerializer.Deserialize<UpdateUserDto>(eventMessage.Payload?.ToString() ?? "");
                        if (userDto != null)
                            await userService.UpdateUserAsync(userDto);
                    }
                    break;

                case "UserDeleted":
                    {
                        var idPayload = JsonSerializer.Deserialize<IdPayload>(eventMessage.Payload?.ToString() ?? "");
                        if (idPayload != null)
                            await userService.DeleteUserAsync(idPayload.Id);
                    }
                    break;
            }
        }, stoppingToken);
    }

    private class IdPayload
    {
        public int Id { get; set; }
    }
}