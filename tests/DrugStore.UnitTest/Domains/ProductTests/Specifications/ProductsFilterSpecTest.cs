using Bogus;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.UnitTest.Builders;

namespace DrugStore.UnitTest.Domains.ProductTests.Specifications;

public sealed class ProductsFilterSpecTest
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData(1, 2, true, "Name", "Product 1")]
    [InlineData(1, 1, true, "Name", "Product 1")]
    [InlineData(2, 1, true, "Name", "Product 2")]
    [InlineData(1, 2, false, "Name", "Product 1")]
    [InlineData(1, 1, false, "Name", "Product 1")]
    [InlineData(2, 1, false, "Name", "Product 2")]
    public void MatchesProductsWithGivenFilter(int skip, int take, bool inStock, string filter, string value)
    {
        // Arrange
        var spec = new ProductsFilterSpec(skip, take, inStock, filter, value);

        // Act
        var result = spec.Evaluate(GetProductCollection());

        // Assert
        Assert.NotNull(result);
    }

    private IEnumerable<Product> GetProductCollection() =>
    [
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = "Product 1",
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.Empty)
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = "Product 2",
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.Empty)
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = "Product 3",
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.NewGuid())
        }
    ];
}