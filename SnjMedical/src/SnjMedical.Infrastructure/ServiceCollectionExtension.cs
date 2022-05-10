using KDS.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnjMedical.Application.Interfaces.Test;
using SnjMedical.Infrastructure.Services.Test;
using SnjMedical.Infrastructure.Sessions;

namespace SnjMedical.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddDistributedMemoryCache()
            .AddTransientServices()
            .AddScopeServices()
            .AddSingletonServices()
            .AddSession(configuration);

        return services;
    }

    public static IServiceCollection AddSession(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration.GetValidOptions<SessionOptions>(SessionOptions.SectionName);

        services.AddSession(configure =>
        {
            configure.Cookie.Name = options.Name;
#if !DEBUG
                 configure.Cookie.Domain = options.Domain;
#endif
            configure.Cookie.HttpOnly = options.HttpOnly;
            configure.Cookie.IsEssential = options.IsEssential;
            configure.IdleTimeout = options.GetIdleTimeout();
        });

        return services;
    }

    public static IServiceCollection AddTransientServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddScopeServices(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>();
        return services;
    }

    public static IServiceCollection AddSingletonServices(this IServiceCollection services)
    {
        return services;
    }
}
