using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Persistence.Repositories;
using MapsterMapper;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(IMapper mapper, IReadRepository<Order> repository)
    : IQueryHandler<GetByIdQuery, Result<OrderDetailVm>>
{
    public async Task<Result<OrderDetailVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new OrderByIdSpec(request.Id);
        var order = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        return Result<OrderDetailVm>.Success(mapper.Map<OrderDetailVm>(order));
    }
}