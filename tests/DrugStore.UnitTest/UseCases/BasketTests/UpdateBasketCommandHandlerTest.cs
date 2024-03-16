﻿using DrugStore.Application.Baskets.Commands.UpdateBasketCommand;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using FluentAssertions;
using Medallion.Threading;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.BasketTests;

public sealed class UpdateBasketCommandHandlerTest
{
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();
    private readonly IDistributedLockProvider _distributedLockProvider = Substitute.For<IDistributedLockProvider>();

    private readonly UpdateBasketCommandHandler _handler;

    private readonly BasketItem _basketItem = new(new(Guid.NewGuid()), "Product Name", 1, 10);

    public UpdateBasketCommandHandlerTest()
    {
        _handler = new(_redisService, _distributedLockProvider);

        var basket = new CustomerBasket
        {
            Id = new(Guid.NewGuid()),
            Items = [_basketItem]
        };

        _redisService.Get<CustomerBasket>(Arg.Any<string>())
            .Returns(basket);
    }

    [Fact]
    public async Task ShouldBeUpdateBasketSuccessfully()
    {
        // Arrange
        var command = new UpdateBasketCommand(new(Guid.NewGuid()), _basketItem);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}