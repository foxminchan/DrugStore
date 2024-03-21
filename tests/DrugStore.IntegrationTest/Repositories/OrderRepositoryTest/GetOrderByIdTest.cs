using System.Text.Json;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.OrderRepositoryTest;

public sealed class GetOrderByIdTest : BaseEfRepoTestFixture
{
    private readonly Repository<Order> _repository;
    private readonly ITestOutputHelper _output;

    public GetOrderByIdTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldGetOrderById()
    {
        // Arrange
        var order = new Order("OD123", null)
        {
            OrderItems =
            [
                new(20.3m, 2, new(), new()),
                new(30.5m, 5, new(), new()),
                new(40.7m, 3, new(), new()),
                new(50.9m, 2, new(), new())
            ]
        };
        await _repository.AddAsync(order);
        var spec = new OrderByIdSpec(order.Id);
        _output.WriteLine("Order: " + JsonSerializer.Serialize(order));

        // Act
        var getOrder = await _repository.FirstOrDefaultAsync(spec);

        // Assert
        Assert.NotNull(getOrder);
        Assert.Equal(order.Id, getOrder.Id);
    }
}