using MediatR;

namespace DrugStore.Domain.SharedKernel;

public abstract record IdempotencyCommand<TResponse>(Guid RequestId) : IRequest<TResponse>, ITxRequest;
