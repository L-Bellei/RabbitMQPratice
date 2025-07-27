using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Entities;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.API.Domain.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RabbitMQ.API.Domain.Services;
public class LoginService(IUserRepository userRepository, IMapper mapper, IOptions<JwtSettingsDto> jwtOptions) : ILoginService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly JwtSettingsDto _jwtSettings = jwtOptions.Value;

    public async Task<AuthResultDto?> AuthenticateAsync(UserLoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Identifier) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = await _userRepository.GetUserByEmailAsync(dto.Identifier.ToLowerInvariant())
                ?? await _userRepository.GetUserByNameAsync(dto.Identifier.ToLowerInvariant());

        if (user != null && VerifyPassword(dto.Password, user.Password))
        {
            var userView = _mapper.Map<UserView>(user);
            var token = GenerateJwtToken(user);

            return new AuthResultDto { User = userView, Token = token };
        }

        return null;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Name, user.Name ?? ""),
        new(ClaimTypes.Email, user.Email ?? "")
    };

        if (!string.IsNullOrEmpty(user.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static bool VerifyPassword(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);

}