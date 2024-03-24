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
        logger.LogError(exception, "[{Handler}] Exception occurred: {ExceptionMessage}", nameof(ExceptionHandler), exception.Message);

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
                await HandleDefaultException(httpContext, exception, cancellationToken);
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
            .Select(e => new ValidationError(
                e.PropertyName,
                e.ErrorMessage,
                StatusCodes.Status400BadRequest.ToString(),
                ValidationSeverity.Info
            )).ToList());
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(validationErrorModel, cancellationToken);
    }

    private static async Task HandleNotFoundException(
        HttpContext httpContext,
        System.Exception notFoundException,
        CancellationToken cancellationToken) =>
        await HandleException(httpContext, notFoundException,
            StatusCodes.Status404NotFound,
            "Not Found",
            notFoundException.Message,
            cancellationToken);

    private static async Task HandleUnauthorizedAccessException(
        HttpContext httpContext,
        System.Exception unauthorizedAccessException,
        CancellationToken cancellationToken) =>
        await HandleException(httpContext, unauthorizedAccessException,
            StatusCodes.Status401Unauthorized,
            "Unauthorized",
            unauthorizedAccessException.Message,
            cancellationToken);

    private static async Task HandleDefaultException(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken) =>
        await HandleException(httpContext, exception,
            StatusCodes.Status500InternalServerError,
            "An error occurred while processing your request",
            exception.Message,
            cancellationToken);

    private static async Task HandleException(
        HttpContext httpContext,
        System.Exception exception,
        int statusCode,
        string title,
        string detail,
        CancellationToken cancellationToken)
    {
        Microsoft.AspNetCore.Mvc.ProblemDetails details = new()
        {
            Status = statusCode,
            Type = exception.GetType().Name,
            Title = title,
            Detail = detail,
            Instance = $"{httpContext.Request.Method}{httpContext.Request.Path}"
        };
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
    }
}