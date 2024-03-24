using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Idempotency.Internal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Idempotency;

public sealed class IdempotentCommandBehavior<TRequest, TResponse>(
    IIdempotencyService idempotencyService,
    ILogger<IdempotentCommandBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotencyCommand<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        const string behavior = nameof(IdempotentCommandBehavior<TRequest, TResponse>);

        logger.LogInformation("[{Behavior}] Handling request={Request} and response={Response}",
            behavior, typeof(TRequest).FullName, typeof(TResponse).FullName);

        if (idempotencyService.RequestExists(request.RequestId))
        {
            logger.LogInformation("[{Behavior}] Request {RequestId} already exists", behavior, request.RequestId);
            return default!;
        }

        var response = await next();
        idempotencyService.CreateRequestForCommand(request.RequestId, request.GetType().Name);
        logger.LogInformation("[{Behavior}] Handled idempotent command {CommandName}", behavior,
            request.GetType().FullName);

        return response;
    }
}