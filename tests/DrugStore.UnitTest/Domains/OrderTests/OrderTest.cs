using Bogus;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.OrderTests;

public sealed class OrderTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeUpdateOrderSuccessfully()
    {
        // Arrange
        var order = new Order(_faker.Random.AlphaNumeric(10), new IdentityId(_faker.Random.Guid()));
        var code = _faker.Random.AlphaNumeric(10);
        var customerId = new IdentityId(_faker.Random.Guid());

        // Act
        order.UpdateOrder(code, customerId);

        // Assert
        order.Code.Should().Be(code);
        order.CustomerId.Should().Be(customerId);
        output.WriteLine("Order: {0}", order);
    }

    [Fact]
    public void ShouldBeInitializeOrderSuccessfully()
    {
        // Arrange
        var code = _faker.Random.AlphaNumeric(10);
        var customerId = new IdentityId(_faker.Random.Guid());

        // Act
        var order = new Order(code, customerId);

        // Assert
        order.Code.Should().Be(code);
        order.CustomerId.Should().Be(customerId);
        output.WriteLine("Order: {0}", order);
    }
}