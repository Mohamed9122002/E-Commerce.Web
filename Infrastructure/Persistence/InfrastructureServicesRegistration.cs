

using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.Identity;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddDbContext<StoreIdentityDbcontext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbcontext>();

            return Services;
        }

    }
}
