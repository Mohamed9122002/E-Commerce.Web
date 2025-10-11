
using AutoMapper;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleware;
using E_Commerce.Web.Extensions;
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
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);
            #endregion
            var app = builder.Build();

            #region DataSeeding
            app.SeedDataBaseAsync();
            #endregion
            #region Configure the HTTP request pipeline
            app.UseCustomExceptionMiddleWare(); 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }

    }
}
