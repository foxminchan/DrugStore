using System.Text.Json;
using DrugStore.Domain.OrderAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.OrderRepositoryTest;

public sealed class CreateOrderTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Order> _repository;

    public CreateOrderTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldCreateOrder()
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
        _output.WriteLine("Order: " + JsonSerializer.Serialize(order));

        // Act
        var getOrder = await _repository.GetByIdAsync(order.Id);

        // Assert
        Assert.NotNull(getOrder);
        Assert.Equal(order.Id, getOrder.Id);
        Assert.Equal(4, getOrder.OrderItems.Count);
    }
}