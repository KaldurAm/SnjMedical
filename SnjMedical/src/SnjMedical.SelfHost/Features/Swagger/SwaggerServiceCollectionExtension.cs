using System.Reflection;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using SnjMedical.Api.Controllers;
using SnjMedical.SelfHost.Features.Swagger.Auth;
using SnjMedical.SelfHost.Features.Swagger.Filters;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SnjMedical.SelfHost.Features.Swagger;

/// <summary>
/// Swagger service collection extension
/// </summary>
internal static class SwaggerServiceCollectionExtension
{
    /// <summary>
    /// Method adding swagger to project
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
        services.AddSingleton(x =>
        {
            var configuration = x.GetService<IConfiguration>();
            if (configuration is null)
            {
                throw new InvalidOperationException("Cannot resolve IConfiguration");
            }
            var swaggerCredentialsOptions = new SwaggerCredentialsOptions(
                configuration.GetValue<string>("SwaggerCredentials:UserName"),
                configuration.GetValue<string>("SwaggerCredentials:Password")
            );

            return swaggerCredentialsOptions;
        });

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(SwaggerConstants.ApiKey,
                new OpenApiSecurityScheme
                {
                    Description =
                        "Authorization header using the API Key scheme. \r\n\r\n" +
                        "Enter your key in the text input below.\r\n\r\n" +
                        "Example: \"api-key 3e2fbe4b-8645-4f9c-8984-23749e37047f\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = SwaggerConstants.ApiKey
                });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerConstants.ApiKey
                        },
                        Scheme = SwaggerConstants.ApiKey,
                        Name = SwaggerConstants.ApiKey,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            };

            options.AddSecurityRequirement(securityRequirement);
            // add operation filters.
            options.OperationFilter<AuthorizationHeaderOperationFilter>(securityRequirement);
            options.OperationFilter<SwaggerDefaultValuesOperationFilter>();

            // add xml documentations.
            options.AddXmlComment(typeof(BaseController).Assembly);
        });

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    private static void AddXmlComment(this SwaggerGenOptions options,
        Assembly assembly)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}
