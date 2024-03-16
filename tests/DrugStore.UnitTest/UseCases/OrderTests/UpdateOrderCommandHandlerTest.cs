using DrugStore.Application.Orders.Commands.UpdateOrderCommand;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class UpdateOrderCommandHandlerTest
{
    private readonly IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

    private readonly List<OrderItemUpdateRequest> _orderItemUpdateRequest;

    public UpdateOrderCommandHandlerTest()
    {
        var order = new Order("Order Name", new(Guid.NewGuid()));

        _orderItemUpdateRequest =
        [
            new(new(Guid.NewGuid()), 1, 10),
            new(new(Guid.NewGuid()), 2, 20),
            new(new(Guid.NewGuid()), 3, 30)
        ];

        _repository.FirstOrDefaultAsync(Arg.Any<OrderByIdSpec>())
            .Returns(order);
    }

    [Fact]
    public async Task ShouldBeUpdateOrderSuccessfully()
    {
        // Arrange
        var command = new UpdateOrderCommand(
            new(Guid.Empty), "Order Name", new(Guid.Empty), _orderItemUpdateRequest
        );
        var handler = new UpdateOrderCommandHandler(_repository);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}