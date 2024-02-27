using Ardalis.Result;
using DrugStore.Application.Categories.ViewModel;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;

namespace DrugStore.FunctionalTest.Components.CategoriesEndpoints;

public sealed class TestGetGetCategoryByIdEndpoint(ApplicationFactory<Program> factory) 
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer().WithCacheContainer();
    private readonly CategoryFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();
    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnCategoryDetails()
    {
        // Arrange
        var client = _factory.CreateClient();
        var category = _faker.Generate(1);
        var id = category[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(category);
        var response = await client.GetAsync($"/api/v1/categories/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.Content.ReadFromJsonAsync<Result<CategoryVm>>();
        item.Should().NotBeNull().And.Match<CategoryVm>(x => x.Id == id);
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/categories/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
