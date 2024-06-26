﻿using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Category;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.CategoriesEndpoints;

public sealed class TestGetCategoriesEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
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
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items =
            await response.Content.ReadFromJsonAsync<ListCategoryResponse>();
        output.WriteLine("Response: {0}", items);
        items.Should().NotBeNull();
    }
}