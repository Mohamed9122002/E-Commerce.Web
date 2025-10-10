
using AutoMapper;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleware;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplementation;
using ServiceImplementation.MappingProfiles;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region  Add services to the container.
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            builder.Services.AddAutoMapper(typeof(ServiceImplementation.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorsResponse;

            });
            #endregion
            var app = builder.Build();

            #region DataSeeding
            var scop = app.Services.CreateScope();
            var ObjectOfDataSeeding = scop.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();

            #endregion
            #region Configure the HTTP request pipeline
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }

    }
}
