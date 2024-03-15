using Bogus;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.BasketTests;

public sealed class BasketItemTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeUpdateBasketItemSuccessfully()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var productName = _faker.Commerce.ProductName();
        var quantity = _faker.Random.Int(1, 100);
        var price = _faker.Random.Decimal(1, 1000);
        var basketItem = new BasketItem(productId, productName, quantity, price);
        var newProductId = new ProductId(_faker.Random.Guid());
        var newProductName = _faker.Commerce.ProductName();
        var newQuantity = _faker.Random.Int(1, 100);
        var newPrice = _faker.Random.Decimal(1, 1000);

        // Act
        basketItem.Update(newProductId, newProductName, newQuantity, newPrice);

        // Assert
        basketItem.Id.Should().Be(newProductId);
        basketItem.ProductName.Should().Be(newProductName);
        basketItem.Quantity.Should().Be(newQuantity);
        basketItem.Price.Should().Be(newPrice);
        output.WriteLine("BasketItem: {0}", basketItem);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenUpdateBasketItemWithNegativeQuantity()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var productName = _faker.Commerce.ProductName();
        var quantity = _faker.Random.Int(1, 100);
        var price = _faker.Random.Decimal(1, 1000);
        var basketItem = new BasketItem(productId, productName, quantity, price);
        var newProductId = new ProductId(_faker.Random.Guid());
        var newProductName = _faker.Commerce.ProductName();
        var newQuantity = -_faker.Random.Int(1, 100);
        var newPrice = _faker.Random.Decimal(1, 1000);

        // Act
        var act = () => basketItem.Update(newProductId, newProductName, newQuantity, newPrice);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenUpdateBasketItemWithNegativePrice()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var productName = _faker.Commerce.ProductName();
        var quantity = _faker.Random.Int(1, 100);
        var price = _faker.Random.Decimal(1, 1000);
        var basketItem = new BasketItem(productId, productName, quantity, price);
        var newProductId = new ProductId(_faker.Random.Guid());
        var newProductName = _faker.Commerce.ProductName();
        var newQuantity = _faker.Random.Int(1, 100);
        var newPrice = -_faker.Random.Decimal(1, 1000);

        // Act
        var act = () => basketItem.Update(newProductId, newProductName, newQuantity, newPrice);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeInitializeBasketItemSuccessfully()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var productName = _faker.Commerce.ProductName();
        var quantity = _faker.Random.Int(1, 100);
        var price = _faker.Random.Decimal(1, 1000);

        // Act
        var basketItem = new BasketItem(productId, productName, quantity, price);

        // Assert
        basketItem.Id.Should().Be(productId);
        basketItem.ProductName.Should().Be(productName);
        basketItem.Quantity.Should().Be(quantity);
        basketItem.Price.Should().Be(price);
        output.WriteLine("BasketItem: {0}", basketItem);
    }
}