using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Domain.Interfaces.Services;

public interface IEnterpriseService
{
    Task<IEnumerable<EnterpriseView>> GetAllEnterprisesAsync();
    Task<EnterpriseView?> GetEnterpriseByIdAsync(int id);
    Task<EnterpriseView?> CreateEnterpriseAsync(EnterpriseDto enterprise);
    Task<EnterpriseView?> UpdateEnterpriseAsync(EnterpriseDto enterprise);
    Task<bool> DeleteEnterpriseAsync(int id);
}
