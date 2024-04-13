using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Basket;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.BasketEndpoints;

public sealed class TestPostBasketEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithCacheContainer();

    private readonly BasketFaker _faker = new();

    public async Task InitializeAsync()
    {
        await _factory.StartContainersAsync();
        await _factory.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        var basket = _faker.Generate(1);

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/baskets", basket);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        output.WriteLine("Response: {0}", response);
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldBeReturnBadRequest(object data)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/baskets", data);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }
}

internal sealed class InvalidData : TheoryData<object>
{
    public InvalidData()
    {
        Add(new { });
        Add(new { ProductId = Guid.NewGuid() });
        Add(new
        {
            ProductId = Guid.NewGuid(),
            Items = new List<BasketItemDto>
            {
                new(new(Guid.NewGuid()), "Product 1", -2, -12.5m, -3.1m),
                new(new(Guid.NewGuid()), "Product 2", 2, -12.5m, -3.1m),
                new(new(Guid.NewGuid()), "Product 3", 2, 12.5m, -3.1m),
                new(new(Guid.NewGuid()), "Product 4", 2, 12.5m, 23.1m)
            }
        });
        Add(new
        {
            ProductId = Guid.NewGuid(),
            Items = new List<BasketItemDto>
            {
                Capacity = 0
            }
        });
    }
}