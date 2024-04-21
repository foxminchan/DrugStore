using MediatR;

namespace DrugStore.Domain.SharedKernel;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITxRequest;