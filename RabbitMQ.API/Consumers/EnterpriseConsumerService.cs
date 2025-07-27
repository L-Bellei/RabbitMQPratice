using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.MessageReceiverBus.Interfaces;
using RabbitMQ.MessageSenderBus.Models;
using System.Text.Json;

namespace RabbitMQ.API.Consumers;
public class EnterpriseConsumerService : BackgroundService
{
    private readonly IRabbitMQMessageReceiver _receiver;
    private readonly IServiceScopeFactory _scopeFactory;

    public EnterpriseConsumerService(
        IRabbitMQMessageReceiver receiver,
        IServiceScopeFactory scopeFactory)
    {
        _receiver = receiver;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _receiver.ReceiveMessagesAsync("enterprise_events", async msg =>
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var enterpriseService = scope.ServiceProvider.GetRequiredService<IEnterpriseService>();
                var eventMessage = JsonSerializer.Deserialize<EventMessage>(msg);

                if (eventMessage == null) return;

                switch (eventMessage.Type)
                {
                    case "EnterpriseCreated":
                        {
                            var dto = JsonSerializer.Deserialize<EnterpriseDto>(eventMessage.Payload?.ToString() ?? "");
                            if (dto != null)
                                await enterpriseService.CreateEnterpriseAsync(dto);
                        }
                        break;

                    case "EnterpriseUpdated":
                        {
                            var dto = JsonSerializer.Deserialize<EnterpriseDto>(eventMessage.Payload?.ToString() ?? "");
                            if (dto != null)
                                await enterpriseService.UpdateEnterpriseAsync(dto);
                        }
                        break;

                    case "EnterpriseDeleted":
                        {
                            var idPayload = JsonSerializer.Deserialize<IdPayload>(eventMessage.Payload?.ToString() ?? "");
                            if (idPayload != null)
                                await enterpriseService.DeleteEnterpriseAsync(idPayload.Id);
                        }
                        break;
                }
            }
        }, stoppingToken);
    }

    private class IdPayload
    {
        public int Id { get; set; }
    }
}