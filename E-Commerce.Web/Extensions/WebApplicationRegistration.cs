using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleware;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> SeedDataBaseAsync (this WebApplication app)
        {
            var scop = app.Services.CreateScope();
            var ObjectOfDataSeeding = scop.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
            return app;
        }
    public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
    }
}
