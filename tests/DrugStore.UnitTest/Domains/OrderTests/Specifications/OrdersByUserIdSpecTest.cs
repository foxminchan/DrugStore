using Bogus;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;

namespace DrugStore.UnitTest.Domains.OrderTests.Specifications;

public sealed class OrdersByUserIdSpecTest
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, 2)]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, 1)]
    [InlineData("00000000-0000-0000-0000-000000000000", 2, 1)]
    public void MatchesOrdersWithGivenUserId(string userId, int skip, int take)
    {
        // Arrange
        var spec = new OrdersByUserIdSpec(new(new(userId)), skip, take);

        // Act
        var result = spec.Evaluate(GetOrderCollection());

        // Assert
        Assert.Equal(take, result.Count());
    }

    private IEnumerable<Order> GetOrderCollection() =>
    [
        new()
        {
            Code = _faker.Random.AlphaNumeric(10),
            CustomerId = new(Guid.Empty)
        },
        new()
        {
            Code = _faker.Random.AlphaNumeric(10),
            CustomerId = new(Guid.Empty)
        },
        new()
        {
            Code = _faker.Random.AlphaNumeric(10),
            CustomerId = new(Guid.NewGuid())
        }
    ];
}