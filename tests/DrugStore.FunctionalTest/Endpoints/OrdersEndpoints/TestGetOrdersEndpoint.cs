using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Order;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestGetOrdersEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer().WithCacheContainer();

    private readonly OrderFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOrders()
    {
        // Arrange
        var client = _factory.CreateClient();
        var orders = _faker.Generate(10);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(orders);
        var response = await client.GetAsync("/api/v1/orders?pageSize=10&pageIndex=1&search=");

        // Assert
        response.EnsureSuccessStatusCode();
        var items =
            await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<OrderDto>>(x => x.Count == 10);
    }

    [Fact]
    public async Task ShouldBeReturnOrdersWithSearch()
    {
        // Arrange
        var client = _factory.CreateClient();
        var orders = _faker.Generate(10);
        var search = orders[0].Code;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(orders);
        var response = await client.GetAsync($"/api/v1/orders?pageSize=10&pageIndex=1&search={search}");

        // Assert
        response.EnsureSuccessStatusCode();
        var items =
            await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<OrderDto>>(x => x.Count == 1);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenPageSizeIsPositiveAndPageIndexIsZero()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/orders?pageSize=10&pageIndex=0&search=");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}