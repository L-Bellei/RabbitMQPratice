using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Interfaces.Services;

namespace RabbitMQ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(ILoginService loginService, ILogger<LoginController> logger) : ControllerBase
{
    private readonly ILoginService _loginService = loginService;
    private readonly ILogger<LoginController> _logger = logger;

    [HttpPost("authenticate")]
    [ProducesResponseType<OkResult>(200)]
    [ProducesResponseType<UnauthorizedResult>(401)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginDto loginDto)
    {
        var userLogged = await _loginService.AuthenticateAsync(loginDto);

        if (userLogged == null)
        {
            _logger.LogWarning("Authentication failed for user {Username}.", loginDto.Identifier);

            return Unauthorized("Invalid username or password.");
        }

        _logger.LogInformation("User {Username} authenticated successfully.", userLogged.Name);

        return Ok(userLogged);
    }
}
