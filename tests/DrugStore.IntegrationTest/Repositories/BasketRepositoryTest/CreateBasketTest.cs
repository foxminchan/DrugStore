using System.Text.Json;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.IntegrationTest.Fixtures;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.BasketRepositoryTest;

public sealed class CreateBasketTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    [Fact]
    public async Task ShouldCreateBasket()
    {
        // Arrange
        var basket = new CustomerBasket()
        {
            Id = new(Guid.NewGuid()),
            Items =
            [
                new(new(Guid.NewGuid()), "Product 1", 10, 8.8m),
                new(new(Guid.NewGuid()), "Product 2", 20, 18.8m),
                new(new(Guid.NewGuid()), "Product 3", 1, 28.8m)
            ]
        };
        IdentityId customerId = new(Guid.NewGuid());
        output.WriteLine("Basket: " + JsonSerializer.Serialize(basket));

        // Act
        var result = _redisService.HashGetOrSet($"user:{customerId}:basket", customerId.ToString(), () => basket);

        // Assert
        Assert.NotNull(result);
    }
}