using Ardalis.GuardClauses;
using Ardalis.Result;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Exception;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {ExceptionMessage}", exception.Message);

        switch (exception)
        {
            case ValidationException { Errors: { } } validationException:
                await HandleValidationException(httpContext, validationException, cancellationToken);
                break;

            case NotFoundException notFoundException:
                await HandleNotFoundException(httpContext, notFoundException, cancellationToken);
                break;

            case UnauthorizedAccessException unauthorizedAccessException:
                await HandleUnauthorizedAccessException(httpContext, unauthorizedAccessException, cancellationToken);
                break;

            default:
                await HandleDefaultException(httpContext, cancellationToken);
                break;
        }

        return true;
    }

    private static async Task HandleValidationException(
        HttpContext httpContext,
        ValidationException validationException,
        CancellationToken cancellationToken)
    {
        var validationErrorModel = Result.Invalid(validationException
            .Errors
            .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage, StatusCodes.Status400BadRequest.ToString(),
                ValidationSeverity.Info))
            .ToList());
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(validationErrorModel, cancellationToken);
    }

    private static async Task HandleNotFoundException(HttpContext httpContext,
        System.Exception notFoundException,
        CancellationToken cancellationToken)
    {
        var notFoundErrorModel = Result.Error(notFoundException.Message);
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(notFoundErrorModel, cancellationToken);
    }

    private static async Task HandleUnauthorizedAccessException(
        HttpContext httpContext,
        System.Exception unauthorizedAccessException,
        CancellationToken cancellationToken)
    {
        var unauthorizedErrorModel =
            Result.Error(unauthorizedAccessException.Message);
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(unauthorizedErrorModel, cancellationToken);
    }

    private static async Task HandleDefaultException(
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var details = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Instance = httpContext.Request.Path
        };
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
    }
}
