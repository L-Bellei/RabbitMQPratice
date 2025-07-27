using Microsoft.EntityFrameworkCore;
using RabbitMQ.API.Domain.Entities;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Infra.Configuration;

namespace RabbitMQ.API.Infra.Repos;

public class EnterpriseRepository(RabbitMQContext context) : IEnterpriseRepository
{
    private readonly RabbitMQContext _context = context;

    public async Task<Enterprise?> GetEnterpriseByIdAsync(int id)
    {
        return await _context.Enterprises
            .AsNoTracking()
            .FirstOrDefaultAsync(enterprise => enterprise.Id == id);
    }
    
    public async Task<IEnumerable<Enterprise>> GetAllEnterprisesAsync()
    {
        return await _context.Enterprises
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Enterprise?> AddEnterpriseAsync(Enterprise enterprise)
    {
        await _context.Enterprises.AddAsync(enterprise);
        await _context.SaveChangesAsync();
    
        return await GetEnterpriseByIdAsync(enterprise.Id);
    }
    
    public async Task<Enterprise?> UpdateEnterpriseAsync(Enterprise enterprise)
    {
        _context.Enterprises.Update(enterprise);
        await _context.SaveChangesAsync();

        return await GetEnterpriseByIdAsync(enterprise.Id);
    }

    public async Task DeleteEnterpriseAsync(int id)
    {
        var enterprise = await GetEnterpriseByIdAsync(id);
        if (enterprise != null)
        {
            _context.Enterprises.Remove(enterprise);
            await _context.SaveChangesAsync();
        }
    }
}
