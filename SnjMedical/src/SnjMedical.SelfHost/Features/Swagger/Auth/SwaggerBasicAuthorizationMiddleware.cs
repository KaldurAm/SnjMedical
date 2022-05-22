using System.Net;
using System.Net.Http.Headers;
using System.Text;

using SnjMedical.Shared.Extensions;

namespace SnjMedical.SelfHost.Features.Swagger.Auth;

/// <summary>
/// swagger basic authorization middleware logic
/// </summary>
internal class SwaggerBasicAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SwaggerCredentialsOptions _options;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="options"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SwaggerBasicAuthorizationMiddleware(RequestDelegate next,
        SwaggerCredentialsOptions options)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// apply logic on context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.Value is null
            || context.Request.Path.Value.Contains("swagger") == false)
        {
            await _next(context);
            return;
        }

        string authorizationHeader = context.Request.Headers["Authorization"];
        if (AuthorizationIsValid(authorizationHeader))
        {
            await _next(context);
            return;
        }

        ReturnUnauthorizedResult(context);
    }

    private bool AuthorizationIsValid(string authorizedHeader)
    {
        if (authorizedHeader.IsNullOrEmpty())
        {
            return false;
        }

        var authorizationHeaderValue = AuthenticationHeaderValue.Parse(authorizedHeader);
        if (!authorizationHeaderValue.Scheme.Equals(
                AuthenticationSchemes.Basic.ToString(),
                StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var credentials = Encoding.UTF8
            .GetString(Convert.FromBase64String(authorizationHeaderValue.Parameter ??
            String.Empty))
            .Split(':', 2);

        if (credentials.Length != 2)
        {
            return false;
        }

        return _options.UserName.Equals(credentials[0]) &&
            _options.Password.Equals(credentials[1]);
    }

    private static void ReturnUnauthorizedResult(HttpContext context)
    {
        context.Response.Headers["WWW-Authenticate"] = "Basic realm=SnjMedical";
        context.Response.StatusCode = 401;
    }
}
