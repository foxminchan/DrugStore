using DrugStore.Domain.ProductAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.ProductTests.DomainEvents;

public sealed class ProductDisabledEventTest
{
    [Fact]
    public void ShouldBeInitializeProductDisabledEventSuccessfully()
    {
        // Arrange
        var id = new ProductId(Guid.NewGuid());
        
        // Act
        var productDisabledEvent = new ProductDisabledEvent(id);
        
        // Assert
        productDisabledEvent.ProductId.Should().Be(id);
    }
}