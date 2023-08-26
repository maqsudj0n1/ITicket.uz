using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Infrastructure.Interceptors;
using ITicket.uz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ITicket.uz.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnect"));
            options.UseLazyLoadingProxies();
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        return services;
    }
}