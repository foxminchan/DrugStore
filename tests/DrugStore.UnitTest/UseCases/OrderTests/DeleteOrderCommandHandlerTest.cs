using DrugStore.Application.Orders.Commands.DeleteOrderCommand;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class DeleteOrderCommandHandlerTest
{
    private readonly IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

    public DeleteOrderCommandHandlerTest()
    {
        var order = new Order("Order Name", new(Guid.NewGuid()));

        _repository.FirstOrDefaultAsync(Arg.Any<OrderByIdSpec>())
            .Returns(order);
    }

    [Fact]
    public async Task ShouldBeDeleteOrderSuccessfully()
    {
        // Arrange
        var command = new DeleteOrderCommand(new(Guid.Empty));
        var handler = new DeleteOrderCommandHandler(_repository);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}