using Ardalis.Specification;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Domain.OrderAggregate.Specifications;

public sealed class OrdersByUserIdSpec : Specification<Order>
{
    public OrdersByUserIdSpec(IdentityId userId, int pageNumber, int pageSize)
        => Query.Where(p => p.CustomerId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(o => o.OrderItems);
}