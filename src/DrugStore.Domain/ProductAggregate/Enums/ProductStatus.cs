﻿using Ardalis.SmartEnum;

namespace DrugStore.Domain.ProductAggregate.Enums;

public sealed class ProductStatus : SmartEnum<ProductStatus>
{
    public static readonly ProductStatus InStock = new(1, nameof(InStock));
    public static readonly ProductStatus OutOfStock = new(2, nameof(OutOfStock));

    private ProductStatus(int id, string name) : base(name, id)
    {
    }
}