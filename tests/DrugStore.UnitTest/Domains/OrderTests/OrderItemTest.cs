using Bogus;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.OrderTests;

public sealed class OrderItemTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeInitializeOrderItemSuccessfully()
    {
        // Arrange
        var price = _faker.Random.Decimal();
        var quantity = _faker.Random.Int(1);
        var productId = new ProductId(_faker.Random.Guid());
        var orderId = new OrderId(_faker.Random.Guid());

        // Act
        var orderItem = new OrderItem(price, quantity, productId, orderId);

        // Assert
        orderItem.Price.Should().Be(price);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.ProductId.Should().Be(productId);
        orderItem.OrderId.Should().Be(orderId);
        output.WriteLine("OrderItem: {0}", orderItem);
    }
}