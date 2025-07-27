using AutoMapper;
using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Services;

public class LoginService(IUserRepository userRepository, IMapper mapper): ILoginService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserView?> AuthenticateAsync(UserLoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Identifier) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = await _userRepository.GetUserByEmailAsync(dto.Identifier.ToLowerInvariant());

        user ??= await _userRepository.GetUserByNameAsync(dto.Identifier.ToLowerInvariant());

        if (user != null && VerifyPassword(dto.Password, user.Password))
            return _mapper.Map<UserView>(user);

        return null;
    }

    private static bool VerifyPassword(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}
