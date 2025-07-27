using RabbitMQ.API.Domain.Dtos;
using RabbitMQ.API.Domain.Dtos.User;
using RabbitMQ.API.Domain.Entities;
using RabbitMQ.API.Domain.Views;

namespace RabbitMQ.API.Configuration;

public static class MapperConfiguration
{
    public static void ConfigureMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<User, UserDto>()
                .ReverseMap();

            cfg.CreateMap<UserDto, UserView>()
                .ReverseMap();

            cfg.CreateMap<CreateUserDto, User>()
                .ReverseMap();

            cfg.CreateMap<UpdateUserDto, User>()
                .ReverseMap();

            cfg.CreateMap<User, UserView>()
                .ReverseMap();

            cfg.CreateMap<Enterprise, EnterpriseDto>()
                .ReverseMap();

            cfg.CreateMap<EnterpriseDto, EnterpriseView>()
                .ReverseMap();

            cfg.CreateMap<Enterprise, EnterpriseView>()
                .ReverseMap();
        });
    }
}
