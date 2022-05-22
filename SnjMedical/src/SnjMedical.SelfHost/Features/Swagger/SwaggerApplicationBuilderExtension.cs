using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SnjMedical.SelfHost.Features.Swagger.Auth;

namespace SnjMedical.SelfHost.Features.Swagger;

internal static class SwaggerApplicationBuilderExtension
{
    internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app,
        IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        app.UseSwaggerBasicAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                options.RoutePrefix = SwaggerConstants.SwaggerRoutePrefix;
            }
        });

        return app;
    }
}
