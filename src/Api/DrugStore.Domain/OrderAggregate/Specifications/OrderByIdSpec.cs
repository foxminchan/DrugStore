﻿using Ardalis.Specification;

namespace DrugStore.Domain.OrderAggregate.Specifications;

public sealed class OrderByIdSpec : Specification<Order>
{
    public OrderByIdSpec(Guid id)
    {
        Query
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems.Select(i => i.Product));
    }
}