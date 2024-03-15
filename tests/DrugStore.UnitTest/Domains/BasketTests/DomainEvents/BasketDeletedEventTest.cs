using Bogus;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate.Primitives;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.BasketTests.DomainEvents;

public sealed class BasketDeletedEventTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeCreateBasketDeletedEventSuccessfully()
    {
        // Arrange
        IDictionary<ProductId, int> items = new Dictionary<ProductId, int>
        {
            { new ProductId(_faker.Random.Guid()), _faker.Random.Int(1, 100) }
        };

        // Act
        var basketDeletedEventResult = new BasketDeletedEvent(items);

        // Assert
        basketDeletedEventResult.Items.Keys.Should().BeEquivalentTo(items.Keys);
        basketDeletedEventResult.Items.Values.Should().BeEquivalentTo(items.Values);
    }
}