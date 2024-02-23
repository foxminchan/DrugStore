using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace DrugStore.Infrastructure.Logging;

public class LoggingBehavior<TRequest, TResponse>(ILogger<Mediator> logger)
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
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(request, null);
                logger.LogInformation("Property {Property} : {@Value}", prop.Name, propValue);
            }
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