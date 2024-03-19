using Bogus;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.BasketTests.DomainEvents;

public sealed class BasketUpdatedEventTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeCreateBasketUpdatedEventSuccessfully()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var quantity = _faker.Random.Int(1, 100);

        // Act
        var basketUpdatedEventResult = new BasketUpdatedEvent(productId, quantity);

        // Assert
        basketUpdatedEventResult.ProductId.Should().Be(productId);
        basketUpdatedEventResult.Quantity.Should().Be(quantity);
    }
}