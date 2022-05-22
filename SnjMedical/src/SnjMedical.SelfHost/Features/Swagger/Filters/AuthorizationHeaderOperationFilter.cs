using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SnjMedical.SelfHost.Features.Swagger.Filters;

/// <summary>
/// authorization header operation filter middleware
/// </summary>
internal class AuthorizationHeaderOperationFilter : IOperationFilter
{
    private readonly OpenApiSecurityRequirement _securityRequirement;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="securityRequirement"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthorizationHeaderOperationFilter(OpenApiSecurityRequirement securityRequirement)
    {
        _securityRequirement = securityRequirement
            ?? throw new ArgumentNullException(nameof(securityRequirement));
    }

    /// <summary>
    /// method applying logic to authorization pipeline
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // check for authorize attribute.
        var hasAuthorize = context.MethodInfo.DeclaringType is { } &&
            (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
            .Any() ||
            context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

        if (!hasAuthorize)
        {
            return;
        }

        operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

        operation.Security.Add(_securityRequirement);
    }
}
