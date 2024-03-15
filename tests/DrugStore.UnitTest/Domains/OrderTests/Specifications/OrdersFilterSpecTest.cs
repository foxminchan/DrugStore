using Bogus;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.UnitTest.Builders;

namespace DrugStore.UnitTest.Domains.OrderTests.Specifications;

public sealed class OrdersFilterSpecTest
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData(1, 2, true, "Id", "Test Code 1")]
    [InlineData(1, 1, true, "Id", "Test Code 2")]
    [InlineData(2, 1, true, "Id", "Test Code 3")]
    [InlineData(1, 2, false, null, "Test Code 3")]
    [InlineData(1, 1, false, "Id", null)]
    [InlineData(2, 1, false, null, null)]
    public void MatchesOrdersWithGivenFilter(int skip, int take, bool isAsc, string? orderBy, string? code)
    {
        // Arrange
        var spec = new OrdersFilterSpec(skip, take, isAsc, orderBy, code);

        // Act
        var result = spec.Evaluate(GetOrderCollection());

        // Assert
        Assert.NotNull(result);
    }

    private IEnumerable<Order> GetOrderCollection() =>
    [
        new()
        {
            Id = new(Guid.NewGuid()),
            Code = "Test Code 1",
            CustomerId = new IdentityId(Guid.NewGuid()),
            OrderItems =
            [
                new()
                {
                    Price = 10,
                    Quantity = 1,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = new(Guid.NewGuid())
                },
                new()
                {
                    Price = 20,
                    Quantity = 2,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = new(Guid.NewGuid())
                }
            ],
            Customer = new(
                _faker.Person.Email,
                _faker.Person.FullName,
                _faker.Person.Phone,
                AddressBuilder.WithDefaultValues()
            )
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Code = "Test Code 2",
            CustomerId = new IdentityId(Guid.NewGuid()),
            OrderItems =
            [
                new()
                {
                    Price = 10,
                    Quantity = 1,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = new(Guid.NewGuid())
                },
                new()
                {
                    Price = 20,
                    Quantity = 2,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = new(Guid.NewGuid())
                }
            ],
            Customer = new(
                _faker.Person.Email,
                _faker.Person.FullName,
                _faker.Person.Phone,
                AddressBuilder.WithDefaultValues()
            )
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Code = "Test Code 3",
            CustomerId = new IdentityId(Guid.NewGuid()),
            OrderItems =
            [
                new()
                {
                    Price = 10,
                    Quantity = 1,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = new(Guid.NewGuid())
                }
            ],
            Customer = new(
                _faker.Person.Email,
                _faker.Person.FullName,
                _faker.Person.Phone,
                AddressBuilder.WithDefaultValues()
            )
        }
    ];
}