using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.HealthCheckEndpoints;

public class TestLivenessEndpoint(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("ConnectionStrings:Postgres",
                    "Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=postgres");
                builder.UseSetting("ConnectionStrings:Redis", "localhost:6379");
            }).CreateClient();

        // Act
        var response = await client.GetAsync("/liveness");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", responseString);
    }

    [Theory]
    [InlineData("ConnectionStrings:Postgres")]
    [InlineData("ConnectionStrings:Redis")]
    public async Task ShouldBeReturnUnhealthy(string connectionString)
    {
        // Arrange
        var client = factory
            .WithWebHostBuilder(builder => builder.UseSetting(connectionString, string.Empty)).CreateClient();

        // Act
        var response = await client.GetAsync("/liveness");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
    }
}