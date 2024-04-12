using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.IntegrationTest.Fixtures;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.BasketRepositoryTest;

public sealed class GetBasketByUserIdTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    [Fact]
    public void ShouldGetBasketByUserId()
    {
        // Arrange
        IdentityId customerId = new(Guid.NewGuid());
        output.WriteLine("CustomerId: " + customerId);

        // Act
        _redisService.HashGetOrSet(
            Arg.Is<string>(key => key == $"user:{customerId}:basket"),
            Arg.Is(customerId.ToString()),
            Arg.Any<Func<CustomerBasket>>()
        );

        // Assert
        _redisService.Received(1).HashGetOrSet(
            Arg.Is<string>(key => key == $"user:{customerId}:basket"),
            Arg.Is(customerId.ToString()),
            Arg.Any<Func<CustomerBasket>>()
        );
    }
}