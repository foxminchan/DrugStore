using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate.ValueObjects;

public sealed class ProductPrice(decimal price = 0, decimal priceSale = 0) : ValueObject
{
    public decimal Price { get; set; } = price;
    public decimal PriceSale { get; set; } = priceSale;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PriceSale;
    }
}