using DrugStore.Domain.SharedKernel;
using MediatR;

namespace DrugStore.Application.Abstractions.Commands;

public abstract record IdempotencyCommand<TResponse>(Guid RequestId) : IRequest<TResponse>, ITxRequest;