using Bogus;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.UnitTest.Builders;

namespace DrugStore.UnitTest.Domains.ProductTests.Specifications;

public sealed class ProductByIdSpecTest
{
    private readonly Faker _faker = new();
    private readonly ProductId _id = new(Guid.NewGuid());

    [Fact]
    public void MatchesProductWithGivenProductId()
    {
        // Arrange
        var spec = new ProductByIdSpec(_id);

        // Act
        var result = spec.Evaluate(GetProductCollection()).FirstOrDefault();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public void MatchesNoProductIfProductIdIsNotPresent()
    {
        // Arrange
        var id = new ProductId(Guid.NewGuid());
        var spec = new ProductByIdSpec(id);

        // Act
        var result = spec.Evaluate(GetProductCollection()).FirstOrDefault();

        // Assert
        Assert.Null(result);
    }

    private IEnumerable<Product> GetProductCollection() =>
    [
        new()
        {
            Id = _id,
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.OutOfStock
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock
        }
    ];
}