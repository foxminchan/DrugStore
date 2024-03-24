using System.Text.Json;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.OrderRepositoryTest;

public sealed class ListOrderTest : BaseEfRepoTestFixture
{
    private readonly List<Order> _orders;
    private readonly ITestOutputHelper _output;
    private readonly Repository<Order> _repository;

    public ListOrderTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
        _orders =
        [
            new("OD123", null)
            {
                OrderItems =
                {
                    new(20.3m, 2, new(), new()),
                    new(30.5m, 5, new(), new()),
                    new(40.7m, 3, new(), new()),
                    new(50.9m, 2, new(), new())
                }
            },
            new("OD124", null)
            {
                OrderItems =
                {
                    new(20.3m, 2, new(), new()),
                    new(30.5m, 5, new(), new()),
                    new(40.7m, 3, new(), new()),
                    new(50.9m, 2, new(), new())
                }
            },
            new("OD125", null)
            {
                OrderItems =
                {
                    new(20.3m, 2, new(), new()),
                    new(30.5m, 5, new(), new()),
                    new(40.7m, 3, new(), new()),
                    new(50.9m, 2, new(), new())
                }
            },
            new("OD126", null)
            {
                OrderItems =
                {
                    new(20.3m, 2, new(), new()),
                    new(30.5m, 5, new(), new()),
                    new(40.7m, 3, new(), new()),
                    new(50.9m, 2, new(), new())
                }
            },
            new("OD127", null)
            {
                OrderItems =
                {
                    new(20.3m, 2, new(), new()),
                    new(30.5m, 5, new(), new()),
                    new(40.7m, 3, new(), new()),
                    new(50.9m, 2, new(), new())
                }
            }
        ];
    }

    [Fact]
    public async Task ShouldListOrder()
    {
        // Arrange
        await _repository.AddRangeAsync(_orders);
        _output.WriteLine("Order: " + JsonSerializer.Serialize(_orders));

        // Act
        var listOrder = await _repository.ListAsync();

        // Assert
        Assert.NotNull(listOrder);
        Assert.Equal(_orders.Count, listOrder.Count);
    }

    [Fact]
    public async Task ShouldListOrderWithOrderItems()
    {
        // Arrange
        await _repository.AddRangeAsync(_orders);
        var spec = new OrdersFilterSpec(1, 10, true, "Id", "OD123");
        _output.WriteLine("Order: " + JsonSerializer.Serialize(_orders));

        // Act
        var listOrder = await _repository.ListAsync(spec);

        // Assert
        Assert.NotNull(listOrder);
        Assert.Single(listOrder);
        Assert.Equal(_orders[0].Id, listOrder[0].Id);
    }

    [Fact]
    public async Task ShouldListEmptyOrder()
    {
        // Arrange
        var order = new List<Order>();
        await _repository.AddRangeAsync(order);
        _output.WriteLine("Order: " + JsonSerializer.Serialize(order));

        // Act
        var listOrder = await _repository.ListAsync();

        // Assert
        Assert.Empty(listOrder);
    }
}