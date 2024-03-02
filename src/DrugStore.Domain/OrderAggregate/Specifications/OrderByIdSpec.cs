using Ardalis.Specification;
using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.Domain.OrderAggregate.Specifications;

public sealed class OrderByIdSpec : Specification<Order>
{
    public OrderByIdSpec(OrderId id)
        => Query.Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .EnableCache(nameof(OrderByIdSpec), id);
}