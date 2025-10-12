using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceImplementation.AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(Provider => () => Provider.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(Provider => () => Provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(Provider => () => Provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(Provider => () => Provider.GetRequiredService<IOrderService>());
            return services;
        }
    }
}
