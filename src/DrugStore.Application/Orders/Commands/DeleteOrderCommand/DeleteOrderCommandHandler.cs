using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed class DeleteOrderCommandHandler(IRepository<Order> repository)
    : ICommandHandler<DeleteOrderCommand, Result>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        await repository.DeleteAsync(order, cancellationToken);
        return Result.Success();
    }
}