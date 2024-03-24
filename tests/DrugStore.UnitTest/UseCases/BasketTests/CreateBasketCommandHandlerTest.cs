using DrugStore.Application.Baskets.Commands.CreateBasketCommand;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using FluentAssertions;
using Medallion.Threading;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.BasketTests;

public sealed class CreateBasketCommandHandlerTest
{
    private readonly IDistributedLockProvider _distributedLockProvider = Substitute.For<IDistributedLockProvider>();

    private readonly CreateBasketCommandHandler _handler;

    private readonly ILogger<CreateBasketCommandHandler>
        _logger = Substitute.For<ILogger<CreateBasketCommandHandler>>();

    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    public CreateBasketCommandHandlerTest() => _handler = new(_redisService, _logger, _distributedLockProvider);

    [Fact]
    public async Task ShouldBeCreateBasketSuccessfully()
    {
        // Arrange
        var command = new CreateBasketCommand(
            Guid.NewGuid(),
            new(Guid.NewGuid()),
            new(new(Guid.NewGuid()), "Product Name", 1, 10)
        );
        _redisService.Get<CustomerBasket>(Arg.Any<string>())
            .Returns(new CustomerBasket());
        _redisService.HashGetOrSet(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<Func<CustomerBasket>>()
        ).Returns(new CustomerBasket { Id = command.CustomerId });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}