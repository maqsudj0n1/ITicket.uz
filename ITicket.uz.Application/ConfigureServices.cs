using FluentValidation;
using ITicket.uz.Application.Commons.Jwt.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace ITicket.uz.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(option =>
            {
                option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<IUserRefreshToken, RefreshToken>();
            services.AddScoped<IJwtToken, JwtToken>();

            return services;
        }
    }
}