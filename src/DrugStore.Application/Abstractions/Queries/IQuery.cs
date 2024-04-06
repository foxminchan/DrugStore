using MediatR;

namespace DrugStore.Application.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>;