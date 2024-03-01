using MediatR;

namespace DrugStore.Domain.SharedKernel;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;