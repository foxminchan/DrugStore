using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.UnitTest.Builders;

public sealed class ProductPriceBuilder
{
    private readonly ProductPrice _productPrice = WithDefaultValues();
    public static decimal Price => 10.00m;
    public static decimal PriceSale => 5.00m;

    public ProductPrice Build() => _productPrice;

    public static ProductPrice WithDefaultValues() => new(Price, PriceSale);
}