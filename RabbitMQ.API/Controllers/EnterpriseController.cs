using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.MessageSenderBus.Interfaces;
using RabbitMQ.MessageSenderBus.Models;

namespace RabbitMQ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnterpriseController(IRabbitMQMessageSender mqSender, ILogger<EnterpriseController> logger) : ControllerBase
{
    private readonly IRabbitMQMessageSender _mqSender = mqSender;
    private readonly ILogger<EnterpriseController> _logger = logger;

    [HttpPost]
    [ProducesResponseType<AcceptedResult>(202)]
    public async Task<IActionResult> CreateEnterpriseAsync([FromBody] EnterpriseDto enterpriseDto)
    {
        if (enterpriseDto == null)
        {
            _logger.LogWarning("Received null enterprise data for creation.");
         
            return BadRequest("Enterprise data cannot be null.");
        }

        var evento = new EventMessage
        {
            Type = "EnterpriseCreated",
            Payload = enterpriseDto
        };

        await _mqSender.SendMessageAsync(evento, "enterprise_events");

        _logger.LogInformation("Enterprise creation event sent for {EnterpriseName}.", enterpriseDto.Name);

        return Accepted(new { status = "Empresa enviada para processamento assíncrono." });
    }

    [HttpPut("{id}")]
    [ProducesResponseType<AcceptedResult>(202)]
    public async Task<IActionResult> UpdateEnterpriseAsync(int id, [FromBody] EnterpriseDto enterpriseDto)
    {
        if (enterpriseDto == null || enterpriseDto.Id != id)
        {
            _logger.LogWarning("Received invalid enterprise data for update. Expected ID: {ExpectedId}, Received ID: {ReceivedId}.", id, enterpriseDto?.Id);

            return BadRequest("Enterprise data is invalid.");
        }

        var evento = new EventMessage
        {
            Type = "EnterpriseUpdated",
            Payload = enterpriseDto
        };

        await _mqSender.SendMessageAsync(evento, "enterprise_events");

        _logger.LogInformation("Enterprise update event sent for {EnterpriseName}.", enterpriseDto.Name);

        return Accepted(new { status = "Empresa enviada para atualização assíncrona." });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<AcceptedResult>(202)]
    public async Task<IActionResult> DeleteEnterpriseAsync(int id)
    {
        var evento = new EventMessage
        {
            Type = "EnterpriseDeleted",
            Payload = new { Id = id }
        };

        await _mqSender.SendMessageAsync(evento, "enterprise_events");

        _logger.LogInformation("Enterprise deletion event sent for ID: {EnterpriseId}.", id);

        return Accepted(new { status = "Empresa enviada para deleção assíncrona." });
    }
}
