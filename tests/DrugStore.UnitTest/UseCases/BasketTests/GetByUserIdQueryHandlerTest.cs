using DrugStore.Application.Baskets.Queries.GetByUserIdQuery;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.BasketTests;

public sealed class GetByUserIdQueryHandlerTest
{
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    public GetByUserIdQueryHandlerTest()
    {
        var basket = new CustomerBasket
        {
            Id = new(Guid.NewGuid()),
            Items =
            [
                new(new(Guid.NewGuid()), "Product Name 1", 1, 10),
                new(new(Guid.NewGuid()), "Product Name 2", 1, 10),
                new(new(Guid.NewGuid()), "Product Name 3", 1, 10)
            ]
        };

        _redisService.Get<CustomerBasket>(Arg.Any<string>())
            .Returns(basket);
    }

    [Fact]
    public async Task ShouldBeGetBasketSuccessfully()
    {
        // Arrange
        var handler = new GetByUserIdQueryHandler(_redisService);
        var query = new GetByUserIdQuery(new(Guid.NewGuid()));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}