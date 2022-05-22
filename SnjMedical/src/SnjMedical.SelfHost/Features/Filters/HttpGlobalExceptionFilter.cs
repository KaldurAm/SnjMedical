using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SnjMedical.Domain.Exceptions;

namespace SnjMedical.SelfHost.Features.Filters;

internal class HttpGlobalExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// handle global http exceptions
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        HandleException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private static void HandleBadRequestException(ExceptionContext context)
    {
        if (context.Exception is not BadRequestException exception)
        {
            return;
        }

        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = exception.Message,
            Detail = exception.Detail
        };

        if (exception.AdditionalProblems is not null)
        {
            foreach (var (key, value) in exception.AdditionalProblems)
                details.Extensions.TryAdd(key, value);
        }

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        if (context.Exception is not ValidationException exception)
        {
            return;
        }

        var details = new ValidationProblemDetails(exception.Errors)
        {
            Instance = context.HttpContext.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Instance = context.HttpContext.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception?.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    private static void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        
    }
}
