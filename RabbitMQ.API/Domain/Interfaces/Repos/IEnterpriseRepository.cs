using RabbitMQ.API.Domain.Entities;

namespace RabbitMQ.API.Domain.Interfaces.Repos;

public interface IEnterpriseRepository
{
    Task<Enterprise?> GetEnterpriseByIdAsync(int id);
    Task<IEnumerable<Enterprise>> GetAllEnterprisesAsync();
    Task<Enterprise?> AddEnterpriseAsync(Enterprise enterprise);
    Task<Enterprise?> UpdateEnterpriseAsync(Enterprise enterprise);
    Task DeleteEnterpriseAsync(int id);
}
