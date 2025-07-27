using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Views;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Domain.Interfaces.Services;
using AutoMapper;
using RabbitMQ.API.Domain.Entities;

namespace RabbitMQ.API.Domain.Services;

public partial class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserView>> GetAllUsersAsync() => 
        _mapper.Map<IEnumerable<UserView>>(await _userRepository.GetAllUsersAsync());

    public async Task<UserView?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return user == null ? null : _mapper.Map<UserView>(user);
    }

    public async Task<UserView?> CreateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        user.Password = HashPassword(userDto.Password);
        
        var createdUser = await _userRepository.AddUserAsync(user);

        return _mapper.Map<UserView>(createdUser);
    }

    public async Task<UserView?> UpdateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(user);

        return updatedUser == null ? null : _mapper.Map<UserView>(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            await _userRepository.DeleteUserAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
