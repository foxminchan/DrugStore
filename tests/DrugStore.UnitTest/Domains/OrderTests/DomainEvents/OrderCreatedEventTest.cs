using DrugStore.Domain.OrderAggregate.DomainEvents;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.OrderTests.DomainEvents;

public sealed class OrderCreatedEventTest
{
    [Fact]
    public void ShouldBeInitializeOrderCreatedEventSuccessfully()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();

        // Act
        var orderCreatedEvent = new OrderCreatedEvent(key);

        // Assert
        orderCreatedEvent.Key.Should().Be(key);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenKeyIsNull()
    {
        // Arrange
        var key = string.Empty;

        // Act
        var act = () => new OrderCreatedEvent(key);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}