using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.CategoriesEndpoints;

public sealed class TestPutCategoryEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer().WithCacheContainer();
    private readonly CategoryFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var category = _faker.Generate(1);
        var id = category[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(category);
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{id}",
            new { Title = "New Name", Link = "https://newlink.com" });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var category = _faker.Generate(1);
        var id = category[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(category);
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{id}",
            new { Title = string.Empty, Link = string.Empty });

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
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{Guid.NewGuid()}",
            new { Title = "New Name", Link = "https://newlink.com" });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}