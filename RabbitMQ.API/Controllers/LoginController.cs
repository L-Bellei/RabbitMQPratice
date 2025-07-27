using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Interfaces.Services;

namespace RabbitMQ.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoginController(ILoginService loginService, ILogger<LoginController> logger) : ControllerBase
{
    private readonly ILoginService _loginService = loginService;
    private readonly ILogger<LoginController> _logger = logger;

    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(AuthResultDto), 200)]
    [ProducesResponseType(typeof(string), 401)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginDto loginDto)
    {
        var authResult = await _loginService.AuthenticateAsync(loginDto);

        if (authResult == null)
        {
            _logger.LogWarning("Authentication failed for user {Username}.", loginDto.Identifier);
            return Unauthorized("Invalid username or password.");
        }

        _logger.LogInformation("User {Username} authenticated successfully.", authResult.User!.Name);

        return Ok(authResult);
    }
}