using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.User;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.UsersEndpoints;

public sealed class TestGetUsersEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnUsers()
    {
        // Arrange
        var client = _factory.CreateClient();
        var users = new ApplicationUserFaker().Generate(10);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(users);
        var response = await client.GetAsync("/api/v1/users?pageSize=10&pageIndex=1&search=");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<UserDto>>(x => x.Count == 10);
    }

    [Fact]
    public async Task ShouldBeReturnUsersWithSearch()
    {
        // Arrange
        var client = _factory.CreateClient();
        var users = new ApplicationUserFaker().Generate(10);
        var search = users[0].Email;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(users);
        var response = await client.GetAsync($"/api/v1/users?pageSize=10&pageIndex=1&search={search}");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<UserDto>>(x => x.Count == 1);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenPageSizeIsPositiveAndPageIndexIsZero()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int pageSize = 10;
        const int pageIndex = 0;

        // Act
        var response = await client.GetAsync($"/api/v1/users?pageSize={pageSize}&pageIndex={pageIndex}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }
}