using MediatR;

namespace DrugStore.Domain.SharedKernel;

public interface IQuery<out TResponse> : IRequest<TResponse>;