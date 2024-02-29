using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Idempotency.Internal;

using MediatR;

using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Idempotency.Behaviors;

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
        logger.LogInformation("Handling idempotent command {CommandName} ({@Command})", request.GetType().Name, request);
        if (idempotencyService.RequestExists(request.RequestId))
        {
            logger.LogWarning("Command {CommandName} was already handled", request.GetType().Name);
            return default!;
        }

        var response = await next();
        idempotencyService.CreateRequestForCommand(request.RequestId, request.GetType().Name);
        logger.LogInformation("Handled idempotent command {CommandName}", request.GetType().Name);

        return response;
    }
}
