﻿using System.Net;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.OrdersEndpoints;

public sealed class TestDeleteOrderEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    private readonly OrderFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        var order = _faker.Generate(1);
        var id = order[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(order);
        var response = await client.DeleteAsync($"/api/v1/orders/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        output.WriteLine("Response: {0}", response);
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/api/v1/orders/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}