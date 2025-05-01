using E_Commerce.Web.Facttories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) => {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;

            });
            return Services;
        }
        public static IServiceCollection  AddJWTService(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                //Options.SaveToken = true;
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTOptions:SecretKey"])),
                };
            });
            return Services;
        }
    }
}
