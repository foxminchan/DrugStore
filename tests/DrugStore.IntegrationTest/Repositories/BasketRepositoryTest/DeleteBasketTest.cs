using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.IntegrationTest.Fixtures;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.BasketRepositoryTest;

public sealed class DeleteBasketTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    [Fact]
    public void ShouldDeleteBasket()
    {
        // Arrange
        IdentityId customerId = new(Guid.NewGuid());
        output.WriteLine("CustomerId: " + customerId);

        // Act
        _redisService.Remove(Arg.Is<string>(key => key == $"user:{customerId}:basket"));

        // Assert
        _redisService.Received(1).Remove(Arg.Is<string>(key => key == $"user:{customerId}:basket"));
    }
}