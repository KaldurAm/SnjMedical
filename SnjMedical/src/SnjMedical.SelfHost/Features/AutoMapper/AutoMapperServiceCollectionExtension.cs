using SnjMedical.Api.Controllers;

namespace SnjMedical.SelfHost.Features.AutoMapper;

public static class AutoMapperServiceCollectionExtension
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseController).Assembly);
        return services;
    }
}
