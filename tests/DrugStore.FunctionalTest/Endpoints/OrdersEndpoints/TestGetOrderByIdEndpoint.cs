using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Order;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestGetOrderByIdEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOrder()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = new OrderFaker().Generate(1);
        var id = order[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(order);
        var response = await client.GetAsync($"/api/v1/orders/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var item = await response.Content.ReadFromJsonAsync<OrderDto>();
        output.WriteLine("Response: {0}", item);
        item.Should()
            .NotBeNull()
            .And
            .Match<OrderDto>(x => x.Id == id);
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/orders/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}