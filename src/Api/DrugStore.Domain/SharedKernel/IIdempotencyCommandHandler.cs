using MediatR;

namespace DrugStore.Domain.SharedKernel;

public interface IIdempotencyCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IdempotencyCommand<TResponse>;
