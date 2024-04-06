using MediatR;

namespace DrugStore.Application.Abstractions.Commands;

public interface IIdempotencyCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IdempotencyCommand<TResponse>;