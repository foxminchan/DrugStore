using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class CreateOrderCommandHandlerTest
{
    private readonly IRepository<Order> _repository = Substitute.For<IRepository<Order>>();
    private readonly ILogger<CreateOrderCommandHandler> _logger = Substitute.For<ILogger<CreateOrderCommandHandler>>();

    private static Order CreateOrder() => new("Order Name", new(Guid.NewGuid()));

    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTest() => _handler = new(_repository, _logger);

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
}