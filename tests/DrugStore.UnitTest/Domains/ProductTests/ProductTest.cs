using Bogus;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.ProductTests;

public sealed class ProductTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeInitializeProductSuccessfully()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        var code = _faker.Random.AlphaNumeric(10);
        var detail = _faker.Lorem.Paragraph();
        var quantity = _faker.Random.Int(1);
        var categoryId = new CategoryId(_faker.Random.Guid());
        var price = new ProductPrice(_faker.Random.Decimal(10, 50), _faker.Random.Decimal(1, 10));

        // Act
        var product = new Product(name, code, detail, quantity, categoryId, price);

        // Assert
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
        product.Quantity.Should().Be(quantity);
        product.CategoryId.Should().Be(categoryId);
        output.WriteLine("Product: {0}", product);
    }

    [Fact]
    public void ShouldBeUpdateProductSuccessfully()
    {
        // Arrange
        var product = new Product(
            _faker.Commerce.ProductName(),
            _faker.Random.AlphaNumeric(10),
            _faker.Lorem.Paragraph(),
            _faker.Random.Int(1),
            new CategoryId(_faker.Random.Guid()),
            new(_faker.Random.Decimal(10, 50), _faker.Random.Decimal(1, 10))
        );

        var name = _faker.Commerce.ProductName();
        var code = _faker.Random.AlphaNumeric(10);
        var detail = _faker.Lorem.Paragraph();
        var quantity = _faker.Random.Int(1);
        var categoryId = new CategoryId(_faker.Random.Guid());
        var price = new ProductPrice(_faker.Random.Decimal(10, 50), _faker.Random.Decimal(1, 10));

        // Act
        product.Update(name, code, detail, quantity, categoryId, price);

        // Assert
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
        product.Quantity.Should().Be(quantity);
        product.CategoryId.Should().Be(categoryId);
        output.WriteLine("Product: {0}", product);
    }

    [Fact]
    public void ShouldBeRemoveStockSuccessfully()
    {
        // Arrange
        var product = new Product(
            _faker.Commerce.ProductName(),
            _faker.Random.AlphaNumeric(10),
            _faker.Lorem.Paragraph(),
            1,
            new CategoryId(_faker.Random.Guid()),
            new(_faker.Random.Decimal(10, 50), _faker.Random.Decimal(1, 10))
        );

        // Act
        product.RemoveStock(1);

        // Assert
        product.Quantity.Should().BeLessThanOrEqualTo(0);
        product.Status.Should().Be(ProductStatus.OutOfStock);
        output.WriteLine("Product: {0}", product);
    }

    [Fact]
    public void ShouldBeAddStockSuccessfully()
    {
        // Arrange
        var product = new Product(
            _faker.Commerce.ProductName(),
            _faker.Random.AlphaNumeric(10),
            _faker.Lorem.Paragraph(),
            9,
            new CategoryId(_faker.Random.Guid()),
            new(_faker.Random.Decimal(10, 50), _faker.Random.Decimal(1, 10))
        );

        // Act
        product.AddStock(1);

        // Assert
        product.Quantity.Should().BeGreaterThanOrEqualTo(10);
        product.Status.Should().Be(ProductStatus.InStock);
        output.WriteLine("Product: {0}", product);
    }
}