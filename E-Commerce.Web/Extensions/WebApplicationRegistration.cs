using DomainLayer.Contracts;
using E_Commerce.Web.CustomeMiddleware;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public  static async Task SeedDatabaseAsync( this WebApplication app)
        {
            // get Container Services  
            // Allowed Dependency Injection Manual  
            var scope = app.Services.CreateScope();
            // Get the Service Provider
            var objectDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectDataSeeding.DataSeedAsync();
        }


        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddlerWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
