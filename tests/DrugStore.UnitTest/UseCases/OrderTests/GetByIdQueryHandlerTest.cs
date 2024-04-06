using DrugStore.Application.Orders.Queries.GetByIdQuery;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.OrderTests;

public sealed class GetByIdQueryHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IReadRepository<Order> _repository = Substitute.For<IReadRepository<Order>>();

    public GetByIdQueryHandlerTest()
    {
        var order = new Order("Order Name", new(Guid.NewGuid()));

        _repository.FirstOrDefaultAsync(Arg.Any<OrderByIdSpec>())
            .Returns(order);
    }

    [Fact]
    public async Task ShouldBeGetOrderSuccessfully()
    {
        // Arrange
        var query = new GetByIdQuery(new(Guid.Empty));
        var handler = new GetByIdQueryHandler(_mapper, _repository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}