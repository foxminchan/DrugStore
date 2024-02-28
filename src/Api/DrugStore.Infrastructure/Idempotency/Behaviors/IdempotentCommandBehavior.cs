using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Idempotency.Internal;

using MediatR;

namespace DrugStore.Infrastructure.Idempotency.Behaviors;

public sealed class IdempotentCommandBehavior<TRequest, TResponse>(IIdempotencyService idempotencyService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IdempotencyCommand<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (idempotencyService.RequestExists(request.RequestId))
            return default!;

        var response = await next();
        idempotencyService.CreateRequestForCommand(request.RequestId, request.GetType().Name);

        return response;
    }
}
