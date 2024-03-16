using DrugStore.Application.Orders.Queries.GetListQuery;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class GetListQueryHandlerTest
{
    private readonly IReadRepository<Order> _repository = Substitute.For<IReadRepository<Order>>();

    public GetListQueryHandlerTest()
    {
        var orders = new List<Order>
        {
            new("Order Name 1", new(Guid.NewGuid())),
            new("Order Name 2", new(Guid.NewGuid())),
            new("Order Name 3", new(Guid.NewGuid()))
        };

        _repository.ListAsync(Arg.Any<OrdersFilterSpec>())
            .Returns(orders);
    }

    [Fact]
    public async Task ShouldBeGetListSuccessfully()
    {
        // Arrange
        var query = new GetListQuery(new("Order Name 1"));
        var handler = new GetListQueryHandler(_repository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}