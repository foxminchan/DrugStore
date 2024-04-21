using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed class DeleteOrderCommandHandler(IRepository<Order> repository)
    : ICommandHandler<DeleteOrderCommand, Result>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        OrderByIdSpec spec = new(request.Id);
        var order = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        await repository.DeleteAsync(order, cancellationToken);
        return Result.Success();
    }
}