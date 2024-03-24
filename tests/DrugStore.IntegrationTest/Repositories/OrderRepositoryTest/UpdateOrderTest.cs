using System.Text.Json;
using DrugStore.Domain.OrderAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.OrderRepositoryTest;

public sealed class UpdateOrderTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Order> _repository;

    public UpdateOrderTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldUpdateOrder()
    {
        // Arrange
        var order = new Order("OD123", null);
        var createdOrder = await _repository.AddAsync(order);
        _output.WriteLine("Order: " + JsonSerializer.Serialize(order));

        // Act
        const string newOrderNumber = "OD456";
        createdOrder.UpdateOrder(newOrderNumber, null);
        await _repository.UpdateAsync(createdOrder);
        var updatedOrder = await _repository.GetByIdAsync(createdOrder.Id);

        // Assert
        Assert.NotNull(updatedOrder);
        Assert.Equal(createdOrder.Id, updatedOrder.Id);
        Assert.NotEqual("OD123", updatedOrder.Code);
    }
}