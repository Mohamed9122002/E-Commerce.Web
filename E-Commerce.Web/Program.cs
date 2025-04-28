
using DomainLayer.Contracts;
using E_Commerce.Web.CustomeMiddleware;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Facttories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplemention;
using ServiceImplemention.MappingProfiles;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region  Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();

            builder.Services.AddWebApplicationServices();


            #endregion
            var app = builder.Build();
            await app.SeedDatabaseAsync();

            #region Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("Watting Response Processing");
            //    Console.WriteLine(RequestContext.Response.Body);
            //});

            app.UseCustomExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAuthorization();
            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
