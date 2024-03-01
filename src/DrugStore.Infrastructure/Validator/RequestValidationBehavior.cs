using System.Diagnostics;
using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Validator;

[DebuggerStepThrough]
public sealed class RequestValidationBehavior<TRequest, TResponse>(
    IServiceProvider serviceProvider,
    ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[{Pipeline}] handle request={RequestData} and response={ResponseData}",
            nameof(RequestValidationBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

        logger.LogDebug(
            "Handled {Request} with content {RequestData}",
            typeof(TRequest).FullName, JsonSerializer.Serialize(request));

        var validators = serviceProvider
                             .GetService<IEnumerable<IValidator<TRequest>>>()?.ToList()
                         ?? throw new InvalidOperationException();

        if (validators.Count != 0)
            await Task.WhenAll(
                validators.Select(v => v.HandleValidationAsync(request))
            );

        var response = await next();

        logger.LogInformation(
            "Handled {FullName} with content {Response}",
            typeof(TResponse).FullName, JsonSerializer.Serialize(response));

        return response;
    }
}