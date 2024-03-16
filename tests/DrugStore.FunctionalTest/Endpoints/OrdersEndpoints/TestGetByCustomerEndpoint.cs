using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Order;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestGetByCustomerEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOrdersByCustomer()
    {
        // Arrange
        var client = _factory.CreateClient();
        var customerId = Guid.NewGuid().ToString();
        var orders = new OrderFaker().Generate(10, customerId);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(orders);
        var response = await client.GetAsync($"/api/v1/orders/customer/{customerId}?pageSize=10&pageIndex=1");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<OrderDto>>(x => x.Count == 10);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenPageSizeIsPositiveAndPageIndexIsZero()
    {
        // Arrange
        var client = _factory.CreateClient();
        var customerId = Guid.NewGuid().ToString();

        // Act
        var response = await client.GetAsync($"/api/v1/orders/customer/{customerId}?pageSize=10&pageIndex=0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeNotFoundWhenCustomerIdIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response =
            await client.GetAsync(
                "/api/v1/orders/customer/00000000-0000-0000-0000-000000000000?pageSize=10&pageIndex=1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}