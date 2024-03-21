using System.Text.Json;
using DrugStore.Domain.OrderAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.OrderRepositoryTest;

public sealed class DeleteOderTest : BaseEfRepoTestFixture
{
    private readonly Repository<Order> _repository;
    private readonly ITestOutputHelper _output;

    public DeleteOderTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldDeleteOrder()
    {
        // Arrange
        var order = new Order("OD123", null);
        await _repository.AddAsync(order);
        _output.WriteLine("Order: " + JsonSerializer.Serialize(order));

        // Act
        await _repository.DeleteAsync(order);

        // Assert
        Assert.DoesNotContain(await _repository.ListAsync(), c => c.Id == order.Id);
    }
}