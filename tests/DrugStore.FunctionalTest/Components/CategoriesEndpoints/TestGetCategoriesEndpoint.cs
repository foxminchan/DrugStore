using System.Net.Http.Json;

using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;

using FluentAssertions;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Components.CategoriesEndpoints;

public sealed class TestGetCategoriesEndpoint(ApplicationFactory<Program> factory)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer().WithCacheContainer();
    private readonly CategoryFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnCategories()
    {
        // Arrange
        var client = _factory.CreateClient();
        var categories = _faker.Generate(10);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(categories);
        var response = await client.GetAsync("/api/v1/categories");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<Result<IEnumerable<CategoryVm>>>();
        items.Should()
            .NotBeNull()
            .And
            .Match<Result<IEnumerable<CategoryVm>>>(x => x.Value.Count() == 10);
    }
}
