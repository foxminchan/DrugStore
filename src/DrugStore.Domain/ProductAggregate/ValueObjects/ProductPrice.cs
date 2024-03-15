using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Domain.ProductAggregate.ValueObjects;

[Owned]
public sealed class ProductPrice(decimal price = 0, decimal priceSale = 0) : ValueObject
{
    public decimal Price { get; set; } = Guard.Against.NegativeOrZero(price);
    public decimal PriceSale { get; set; } = Guard.Against.NegativeOrZero(priceSale);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PriceSale;
    }
}