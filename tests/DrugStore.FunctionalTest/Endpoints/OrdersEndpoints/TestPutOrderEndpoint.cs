using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestPutOrderEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    private readonly OrderFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = _faker.Generate(1);
        var id = order[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(order);
        var response = await client.PutAsJsonAsync($"/api/v1/orders/{id}",
            new
            {
                order[0].Code,
                order[0].CustomerId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = _faker.Generate(1);
        var id = order[0].Id;

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/orders/{id}",
            new
            {
                Code = "",
                order[0].CustomerId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/orders/{Guid.NewGuid()}",
            new
            {
                Code = "New Code",
                CustomerId = Guid.NewGuid()
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnConflict()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = _faker.Generate(1);
        var id = order[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(order);
        var response = await client.PutAsJsonAsync($"/api/v1/orders/{id}",
            new
            {
                order[0].Code,
                CustomerId = Guid.NewGuid()
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        output.WriteLine("Response: {0}", response);
    }
}