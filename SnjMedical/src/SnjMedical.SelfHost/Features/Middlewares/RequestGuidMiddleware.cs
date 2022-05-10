namespace SnjMedical.SelfHost.Features.Middlewares;

/// <summary>
/// add request guid to each request
/// </summary>
public class RequestGuidMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary></summary>
    public RequestGuidMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary></summary>
    public Task InvokeAsync(HttpContext context)
    {
        Populate(context);
        return _next(context);
    }

    private void Populate(HttpContext context)
    {
        var requestGuidName = "requestGuid";
        if (context.Items.ContainsKey(requestGuidName))
            return;
        var requestGuid = Guid.NewGuid();
        context.Items.Add(requestGuidName, requestGuid);
    }
}
