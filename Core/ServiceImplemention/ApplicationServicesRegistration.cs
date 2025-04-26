using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplemention.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
  public static  class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
