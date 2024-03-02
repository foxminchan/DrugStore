using System.Diagnostics;
using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Logging;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

            var myType = request.GetType();
            var props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
                logger.LogInformation("Property {Property} : {@Value}", prop.Name, prop.GetValue(request, null));
        }

        var sw = Stopwatch.StartNew();
        var response = await next();

        logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms", typeof(TRequest).Name, response,
            sw.ElapsedMilliseconds);

        sw.Stop();

        var timeTaken = sw.Elapsed;

        if (timeTaken.Seconds > 3)
            logger.LogWarning("Request {RequestName} took {TimeTaken} to complete", typeof(TRequest).Name, timeTaken);

        return response;
    }
}