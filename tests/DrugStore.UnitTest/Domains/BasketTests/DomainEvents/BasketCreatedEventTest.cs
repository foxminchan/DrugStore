﻿using Bogus;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.BasketTests.DomainEvents;

public sealed class BasketCreatedEventTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeCreateBasketCreatedEventSuccessfully()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var quantity = _faker.Random.Int(1, 100);

        // Act
        var basketCreatedEventResult = new BasketCreatedEvent(productId, quantity);

        // Assert
        basketCreatedEventResult.ProductId.Should().Be(productId);
        basketCreatedEventResult.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenCreateBasketCreatedEventWithNegativeQuantity()
    {
        // Arrange
        var productId = new ProductId(_faker.Random.Guid());
        var quantity = _faker.Random.Int(-100, -1);

        // Act
        var act = () => new BasketCreatedEvent(productId, quantity);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}