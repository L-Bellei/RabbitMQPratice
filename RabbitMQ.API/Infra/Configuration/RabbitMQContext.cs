using Microsoft.EntityFrameworkCore;
using RabbitMQ.API.Domain.Entities;

namespace RabbitMQ.API.Infra.Configuration;

public class RabbitMQContext(DbContextOptions<RabbitMQContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Enterprise> Enterprises { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RabbitMQContext).Assembly);
    }   
}
