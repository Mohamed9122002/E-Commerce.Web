using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleware;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync (this WebApplication app)
        {
            var scop = app.Services.CreateScope();
            var ObjectOfDataSeeding = scop.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
        }
    public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
    }
}
