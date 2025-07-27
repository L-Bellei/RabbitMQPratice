using Microsoft.EntityFrameworkCore;
using RabbitMQ.API.Domain.Entities;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Infra.Configuration;

namespace RabbitMQ.API.Infra.Repos;

public class UserRepository(RabbitMQContext context) : IUserRepository
{
    private readonly RabbitMQContext _context = context;

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return await GetUserByIdAsync(user.Id);
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        return await GetUserByIdAsync(user.Id);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);

        if (user != null)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _context.Users
                .AsNoTracking()
                .Where(user => user.Email == email)
                .FirstOrDefaultAsync();

    public async Task<User?> GetUserByNameAsync(string name) =>
       await _context.Users
               .AsNoTracking()
               .Where(user => user.Name == name)
               .FirstOrDefaultAsync();
}
