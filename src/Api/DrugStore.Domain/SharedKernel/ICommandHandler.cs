using MediatR;

namespace DrugStore.Domain.SharedKernel;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;
