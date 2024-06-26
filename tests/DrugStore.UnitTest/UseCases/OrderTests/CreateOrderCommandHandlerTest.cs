﻿using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Persistence.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class CreateOrderCommandHandlerTest
{
    private readonly CreateOrderCommandHandler _handler;
    private readonly ILogger<CreateOrderCommandHandler> _logger = Substitute.For<ILogger<CreateOrderCommandHandler>>();
    private readonly IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

    public CreateOrderCommandHandlerTest() => _handler = new(_repository, _logger);

    private static Order CreateOrder() => new("Order Name", new(Guid.NewGuid()));

    [Fact]
    public async Task ShouldBeCreateOrderSuccessfully()
    {
        // Arrange
        var command = new CreateOrderCommand(
            Guid.NewGuid(),
            "Order Name",
            new(Guid.NewGuid()),
            [
                new(new(Guid.NewGuid()), 1, 10.0m),
                new(new(Guid.NewGuid()), 2, 20.0m),
                new(new(Guid.NewGuid()), 3, 30.0m)
            ]
        );
        _repository.AddAsync(Arg.Any<Order>())
            .Returns(Task.FromResult(CreateOrder()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldNotCreateOrder(CreateOrderCommand command)
    {
        // Arrange
        _repository.AddAsync(Arg.Any<Order>()).Returns(Task.FromResult(CreateOrder()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}

internal sealed class InvalidData : TheoryData<CreateOrderCommand>
{
    public InvalidData()
    {
        Add(new(Guid.NewGuid(), string.Empty, new(Guid.Empty), [
            new(new(Guid.NewGuid()), -1, 10.0m),
            new(new(Guid.NewGuid()), -2, 20.0m),
            new(new(Guid.NewGuid()), -3, 30.0m)
        ]));
        Add(new(Guid.Empty, "Order Name", new(Guid.NewGuid()), [
            new(new(Guid.NewGuid()), 1, 10.0m),
            new(new(Guid.NewGuid()), 2, -20.0m),
            new(new(Guid.NewGuid()), 3, 30.0m)
        ]));
        Add(new(Guid.NewGuid(), string.Empty, new(Guid.Empty), [
            new(new(Guid.NewGuid()), 0, 0.0m),
            new(new(Guid.NewGuid()), 0, 0.0m),
            new(new(Guid.NewGuid()), 0, 0.0m)
        ]));
    }
}