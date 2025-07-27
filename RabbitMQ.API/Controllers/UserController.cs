using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.MessageSenderBus.Interfaces;
using RabbitMQ.MessageSenderBus.Models;

namespace RabbitMQ.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(IRabbitMQMessageSender mqSender, ILogger<EnterpriseController> logger, IUserService userService) : ControllerBase
{
    private readonly IRabbitMQMessageSender _mqSender = mqSender;
    private readonly ILogger<EnterpriseController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpPost]
    [ProducesResponseType<AcceptedResult>(202)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            _logger.LogWarning("Received null user data for creation.");

            return BadRequest("User data is null.");
        }

        var evento = new EventMessage
        {
            Type = "UserCreated",
            Payload = userDto
        };

        await _mqSender.SendMessageAsync(evento, "user_events");

        _logger.LogInformation("User creation event sent for {UserName}.", userDto.Name);

        return Accepted(new { status = "Usuário enviado para processamento assíncrono." });
    }

    [HttpPut("{id}")]
    [ProducesResponseType<AcceptedResult>(202)]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserDto userDto)
    {
        if (userDto == null || userDto.Id != id)
        {
            _logger.LogWarning("Received invalid user data for update. Expected ID: {ExpectedId}, Received ID: {ReceivedId}.", id, userDto?.Id);

            return BadRequest("User data is invalid.");
        }

        var evento = new EventMessage
        {
            Type = "UserUpdated",
            Payload = userDto
        };

        await _mqSender.SendMessageAsync(evento, "user_events");

        _logger.LogInformation("User update event sent for {UserName}.", userDto.Name);

        return Accepted(new { status = "Usuário enviado para atualização assíncrona." });
    }

    [HttpGet("{id}")]
    [ProducesResponseType<AcceptedResult>(202)]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var userFinded = await _userService.GetUserByIdAsync(id);

        _logger.LogInformation("User finded for user ID {UserId}.", id);

        return Ok(new { data = userFinded });
    }

    [HttpGet]
    [ProducesResponseType<AcceptedResult>(200)]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var usersFinded = await _userService.GetAllUsersAsync();

        _logger.LogInformation("All users are finded: {Users}", JsonConvert.SerializeObject(usersFinded, Formatting.Indented));

        return Ok(new { data = usersFinded });
    }


    [HttpDelete("{id}")]
    [ProducesResponseType<AcceptedResult>(202)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var evento = new EventMessage
        {
            Type = "UserDeleted",
            Payload = new { Id = id }
        };

        await _mqSender.SendMessageAsync(evento, "user_events");

        _logger.LogInformation("User deletion event sent for user ID {UserId}.", id);

        return Accepted(new { status = "Usuário enviado para deleção assíncrona." });
    }
}
