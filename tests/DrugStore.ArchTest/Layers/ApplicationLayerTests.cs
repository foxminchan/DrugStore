using DrugStore.Application;
using DrugStore.ArchTest.Constants;
using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest.Layers;

public sealed class ApplicationLayerTests
{
    [Fact]
    public void ApplicationShouldNotHaveDependencyOnInfrastructure()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(Namespace.Infrastructure)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }

    [Fact]
    public void ApplicationShouldNotHaveDependencyOnPersistence()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(Namespace.Persistence)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }
}