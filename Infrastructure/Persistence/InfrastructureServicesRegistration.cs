using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.DbContexts;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;


namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<IdentityStoreDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
              return   ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
