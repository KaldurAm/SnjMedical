namespace SnjMedical.SelfHost.Features.Swagger.Auth;

/// <summary>
/// swagger basic authorization middleware extension
/// </summary>
public static class SwaggerBasicAuthorizationMiddlewareExtension
{
    /// <summary>
    /// method apply basic authorization in pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerBasicAuthorization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SwaggerBasicAuthorizationMiddleware>();
    }
}
