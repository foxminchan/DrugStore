﻿using Ardalis.Specification;

namespace DrugStore.Domain.Order.Specifications;

public sealed class OrderByIdSpec : Specification<Order>
{
    public OrderByIdSpec(Guid id)
    {
        Query
            .Where(o => o.Id == id)
            .Include(o => o.Items.Select(i => i.Product));
    }
}