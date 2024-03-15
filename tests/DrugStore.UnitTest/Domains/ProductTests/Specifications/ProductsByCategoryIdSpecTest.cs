using Bogus;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.UnitTest.Builders;

namespace DrugStore.UnitTest.Domains.ProductTests.Specifications;

public sealed class ProductsByCategoryIdSpecTest
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, 2)]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, 1)]
    [InlineData("00000000-0000-0000-0000-000000000000", 2, 1)]
    public void MatchesProductsWithGivenCategoryId(string categoryId, int skip, int take)
    {
        // Arrange
        var spec = new ProductsByCategoryIdSpec(new(new(categoryId)), skip, take);

        // Act
        var result = spec.Evaluate(GetProductCollection());

        // Assert
        Assert.Equal(take, result.Count());
    }

    private IEnumerable<Product> GetProductCollection() =>
    [
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.Empty)
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.Empty)
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = _faker.Commerce.ProductName(),
            ProductCode = _faker.Random.AlphaNumeric(10),
            Quantity = _faker.Random.Number(1, 100),
            Price = ProductPriceBuilder.WithDefaultValues(),
            Status = ProductStatus.InStock,
            CategoryId = new(Guid.NewGuid())
        }
    ];
}