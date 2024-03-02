using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Domain.ProductAggregate.ValueObjects;

[Owned]
public sealed class ProductPrice(
    decimal originalPrice,
    decimal price,
    decimal? priceSale) : ValueObject
{
    public decimal OriginalPrice { get; set; } = originalPrice;
    public decimal Price { get; set; } = price;
    public decimal? PriceSale { get; set; } = priceSale;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OriginalPrice;
        yield return Price;
        yield return PriceSale ?? 0;
    }
}