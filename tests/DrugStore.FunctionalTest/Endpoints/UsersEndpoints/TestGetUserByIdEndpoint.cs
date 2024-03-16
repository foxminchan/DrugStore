using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.User;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.UsersEndpoints;

public sealed class TestGetUserByIdEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = new ApplicationUserFaker().Generate(1);
        var id = user[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(user);
        var response = await client.GetAsync($"/api/v1/users/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var item = await response.Content.ReadFromJsonAsync<UserDto>();
        output.WriteLine("Response: {0}", item);
        item.Should().NotBeNull();
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/users/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}