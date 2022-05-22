using SnjMedical.Api.Controllers;

namespace SnjMedical.SelfHost.Features.AutoMapper;

internal static class AutoMapperServiceCollectionExtension
{
    internal static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseController).Assembly);
        return services;
    }
}
