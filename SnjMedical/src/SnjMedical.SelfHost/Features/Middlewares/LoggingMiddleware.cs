using System.Diagnostics;
using System.Text;

namespace SnjMedical.SelfHost.Features.Middlewares;

/// <summary>
/// add request guid and scope to logs
/// </summary>
internal class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    /// <summary></summary>
    public LoggingMiddleware(RequestDelegate next,
        ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary></summary>
    internal async Task InvokeAsync (HttpContext context)
    {
        context.Items.TryGetValue("requestGuid", out object? requestGuid);
        var logContext = new Dictionary<string, object>
        {
            ["requestGuid"] = requestGuid ?? "",
        };
        using (_logger.BeginScope(logContext))
        {
            await ExecuteLogContext(context);
        }
    }

    private async Task ExecuteLogContext (HttpContext context)
    {
        var logRequest = context.Request.Path.StartsWithSegments("/api/healthcheck");
        var body = await GetRequestBody(context.Request);
        if (logRequest)
        {
            _logger.LogInformation("{msg} {method} {path} {queryString} {body} {connection} {headers}",
                $"=> {context.Request.Method} {context.Request.Path}?{context.Request.QueryString.Value}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString.Value,
                body,
                "", // context.Connection.ToLog(),
                context.Request.Headers
             );
        }

        // Copy a pointer to the original response body stream
        var originalBodyStream = context.Response.Body;

        // Create a new memory stream...
        using (var responseBody = new MemoryStream())
        {
            // ...and use that for the temporary response body
            context.Response.Body = responseBody;

            // Continue down the Middleware pipeline, eventually returning to this class
            Stopwatch sw = Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            var logLevel = LogLevel.Information;

            if (context.Response.StatusCode >= 400)
            {
                logLevel = LogLevel.Error;
            }
            else if (context.Response.StatusCode >= 300)
            {
                logLevel = LogLevel.Warning;
            }

            var response = await GetResponseBody(context.Response);

            if (!string.IsNullOrEmpty(response) && response.Length > 8192)
            {
                response = $"Too long response ({response.Length / 1024} Kb)";
            }

            if (logRequest)
            {
                _logger.Log(logLevel,
                    "{msg} {Method} {path} {queryString} {HttpStatusCode} {body} {proctime}",
                    $"<= {context.Request.Method} {context.Request.Path}?{context.Request.QueryString.Value}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString.Value,
                    context.Response.StatusCode,
                    response, // ,
                    sw.ElapsedMilliseconds
                );
            }

            // Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> GetResponseBody(HttpResponse response)
    {
        // We need to read the response stream from the beginning...
        response.Body.Seek(0, SeekOrigin.Begin);

        // ...and copy it into a string
        string text = await new StreamReader(response.Body).ReadToEndAsync();

        // We need to reset the reader for the response so that the client can read it.
        response.Body.Seek(0, SeekOrigin.Begin);

        // Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
        return text;
    }

    private async Task<string> GetRequestBody(HttpRequest request)
    {
        // This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();

        // We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        // ...Then we copy the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        // We convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        // ..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
        request.Body.Position = 0;

        return bodyAsText;
    }
}
