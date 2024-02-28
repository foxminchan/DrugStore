﻿namespace DrugStore.Domain.BasketAggregate;

public sealed class CustomerBasket(Guid id)
{
    public Guid Id { get; set; } = id;
    public List<BasketItem> Items { get; set; } = [];
}