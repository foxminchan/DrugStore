﻿using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate.DomainEvents;

public sealed class BasketUpdatedEvent(ProductId productId, int quantity) : DomainEventBase
{
    public ProductId ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}