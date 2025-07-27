using Microsoft.EntityFrameworkCore;
using RabbitMQ.API.Consumers;
using RabbitMQ.API.Domain.Interfaces.Repos;
using RabbitMQ.API.Domain.Interfaces.Services;
using RabbitMQ.API.Domain.Services;
using RabbitMQ.API.Infra.Configuration;
using RabbitMQ.API.Infra.Repos;
using RabbitMQ.MessageSenderBus.Extensions;
using RabbitMQ.MessageReceiverBus.Extensions;

namespace RabbitMQ.API.Configuration;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RabbitMQContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEnterpriseService, EnterpriseService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddRabbitMqSenderBus();
        services.AddRabbitMqReceiverBus();

        services.AddHostedService<UserConsumerService>();
        services.AddHostedService<EnterpriseConsumerService>();
    }

}
