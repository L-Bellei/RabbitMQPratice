using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Entities;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.API.Domain.Views;
using AutoMapper;

namespace RabbitMQ.API.Domain.Services;

public class EnterpriseService(IEnterpriseRepository enterpriseRepository, IMapper mapper) : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository = enterpriseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<EnterpriseView>> GetAllEnterprisesAsync()
    {
        var enterprises = await _enterpriseRepository.GetAllEnterprisesAsync();

        return [.. enterprises.Select(e => _mapper.Map<EnterpriseView>(e))];
    }

    public async Task<EnterpriseView?> GetEnterpriseByIdAsync(int id)
    {
        var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(id);
        return enterprise == null ? null : _mapper.Map<EnterpriseView>(enterprise);
    }

    public async Task<EnterpriseView?> CreateEnterpriseAsync(EnterpriseDto enterpriseDto)
    {
        if (enterpriseDto == null)
        {
            throw new ArgumentNullException(nameof(enterpriseDto), "Enterprise data cannot be null.");
        }
        var enterprise = _mapper.Map<Enterprise>(enterpriseDto);
        var createdEnterprise = await _enterpriseRepository.AddEnterpriseAsync(enterprise);
        
        return createdEnterprise == null ? null : _mapper.Map<EnterpriseView>(createdEnterprise);
    }

    public async Task<EnterpriseView?> UpdateEnterpriseAsync(EnterpriseDto enterpriseDto)
    {
        if (enterpriseDto == null)
        {
            throw new ArgumentNullException(nameof(enterpriseDto), "Enterprise data cannot be null.");
        }
        var existingEnterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(enterpriseDto.Id);
        if (existingEnterprise == null)
        {
            return null;
        }
        var enterprise = _mapper.Map<Enterprise>(enterpriseDto);
        var updatedEnterprise = await _enterpriseRepository.UpdateEnterpriseAsync(enterprise);
        
        return updatedEnterprise == null ? null : _mapper.Map<EnterpriseView>(updatedEnterprise);
    }

    public async Task<bool> DeleteEnterpriseAsync(int id)
    {
        var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(id);
        if (enterprise == null)
        {
            return false;
        }
        await _enterpriseRepository.DeleteEnterpriseAsync(id);
        return true;
    }
}
