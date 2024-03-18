using DrugStore.Application.Orders.Queries.GetListByUserIdQuery;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class GetListByUserIdQueryHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IReadRepository<Order> _repository = Substitute.For<IReadRepository<Order>>();

    public GetListByUserIdQueryHandlerTest()
    {
        var orders = new List<Order>
        {
            new("Order Name 1", new(Guid.NewGuid())),
            new("Order Name 2", new(Guid.NewGuid())),
            new("Order Name 3", new(Guid.NewGuid()))
        };

        _repository.ListAsync(Arg.Any<OrdersByUserIdSpec>())
            .Returns(orders);
    }

    [Fact]
    public async Task ShouldBeGetListByUserIdSuccessfully()
    {
        // Arrange
        var query = new GetListByUserIdQuery(new(Guid.Empty), new());
        var handler = new GetListByUserIdQueryHandler(_mapper, _repository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}