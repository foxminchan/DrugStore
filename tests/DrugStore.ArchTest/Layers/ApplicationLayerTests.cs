using DrugStore.ArchTest.Fixtures;
using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest.Layers;

public sealed class ApplicationLayerTests
{
    [Fact]
    public void ApplicationShouldNotHaveDependencyOnInfrastructure()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(Namespace.InfrastructureNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }

    [Fact]
    public void ApplicationShouldNotHaveDependencyOnPersistence()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(Namespace.PersistenceNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }
}