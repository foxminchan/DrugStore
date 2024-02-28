using System.Net;
using System.Net.Http.Json;

using Bogus;

using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fixtures;

using FluentAssertions;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Components.CategoriesEndpoints;

public sealed class TestPostCategoryEndpoint(ApplicationFactory<Program> factory)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

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
        var faker = new Faker();

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/categories", new
        {
            Title = faker.Commerce.Categories(1)[0],
            Link = faker.Internet.Url()
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/categories", new
        {
            Title = string.Empty,
            Link = string.Empty
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
