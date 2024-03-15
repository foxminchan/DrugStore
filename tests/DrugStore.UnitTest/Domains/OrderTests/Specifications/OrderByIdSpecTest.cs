using Bogus;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.UnitTest.Builders;

namespace DrugStore.UnitTest.Domains.OrderTests.Specifications;

public sealed class OrderByIdSpecTest
{
    private readonly OrderId _orderId = new(Guid.NewGuid());
    private readonly Faker _faker = new();

    [Fact]
    public void MatchesOrderWithGivenOrderId()
    {
        // Arrange
        var spec = new OrderByIdSpec(_orderId);

        // Act
        var result = spec.Evaluate(GetOrderCollection()).FirstOrDefault();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.OrderItems);
        Assert.NotNull(result.Customer);
        Assert.Equal(_orderId, result.Id);
    }

    [Fact]
    public void MatchesNoOrderIfOrderIdIsNotPresent()
    {
        // Arrange
        var id = new OrderId(Guid.NewGuid());
        var spec = new OrderByIdSpec(id);

        // Act
        var result = spec.Evaluate(GetOrderCollection()).FirstOrDefault();

        // Assert
        Assert.Null(result);
    }

    private IEnumerable<Order> GetOrderCollection() =>
    [
        new()
        {
            Id = _orderId,
            Code = _faker.Random.AlphaNumeric(10),
            CustomerId = new IdentityId(Guid.NewGuid()),
            OrderItems =
            [
                new()
                {
                    Price = 10,
                    Quantity = 1,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = _orderId
                },
                new()
                {
                    Price = 20,
                    Quantity = 2,
                    ProductId = new(Guid.NewGuid()),
                    OrderId = _orderId
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
            Code = _faker.Random.AlphaNumeric(10),
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
            Code = _faker.Random.AlphaNumeric(10),
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