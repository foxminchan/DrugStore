using System.Net;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.HealthCheckEndpoints;

public class TestReadinessEndpoint(ApplicationFactory<Program> factory)
    : IClassFixture<ApplicationFactory<Program>>
{
    [Fact]
    public async Task ShouldBeReturnOk()
    {
        try
        {
            // Arrange
            await factory
                .WithDbContainer()
                .WithCacheContainer()
                .StartContainersAsync();

            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/hc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        finally
        {
            await factory.StopContainersAsync();
        }
    }

    [Fact]
    public async Task ShouldBeReturnUnhealthy()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/hc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
    }
}