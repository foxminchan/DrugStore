using System.Net;
using System.Net.Http.Json;
using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestPostOrderEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync()
    {
        await _factory.StartContainersAsync();
        await _factory.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Theory]
    [InlineData("1d6f6e4d-0f6e-4f3b-8e3e-3f3e3e3e3e3e")] // Change the value to a valid customer id
    public async Task ShouldBeReturnCreated(string customerId)
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = new
        {
            Code = "123",
            CustomerId = customerId,
            Items = new[]
            {
                new
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1,
                    Price = 1000
                }
            }
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/orders", order);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        output.WriteLine("Response: {0}", response);
    }

    [Theory(DisplayName = "Should be return bad request")]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldBeReturnBadRequest(object order)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/orders", order);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnConflict()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = new
        {
            Code = "123",
            CustomerId = Guid.NewGuid(),
            Items = new[]
            {
                new
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1,
                    Price = 1000
                }
            }
        };

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/orders", order);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        output.WriteLine("Response: {0}", response);
    }
}

internal sealed class InvalidData : TheoryData<object>
{
    public InvalidData()
    {
        Add(new { Code = string.Empty });
        Add(new { Code = string.Empty, CustomerId = Guid.Empty });
        Add(new { Code = "123", CustomerId = Guid.Empty });
        Add(new { Code = "123", Items = new List<OrderItemCreateRequest>(), CustomerId = Guid.Empty });
        Add(new
        {
            Code = "12345678910111213141516",
            Items = new List<OrderItemCreateRequest>(),
            CustomerId = Guid.Empty
        });
        Add(new
        {
            Code = "12345678910111213141516",
            Items = new List<OrderItemCreateRequest>
            {
                new(new(Guid.Empty), 0, -1),
                new(new(Guid.Empty), -1, -1),
                new(new(Guid.Empty), 0, 0)
            },
            CustomerId = Guid.Empty
        });
    }
}