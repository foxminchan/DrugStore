using DrugStore.ArchTest.Fixtures;
using DrugStore.Persistence;
using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest.Layers;

public sealed class PersistenceLayerTest
{
    [Fact]
    public void PersistenceLayerShouldNotHaveDependencyOnAnyLayer()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        string[] layers =
        [
            Namespace.DomainNamespace,
            Namespace.InfrastructureNamespace,
            Namespace.ApplicationNamespace,
            Namespace.PresentationNamespace,
            Namespace.PersistenceNamespace
        ];

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(layers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }

    [Fact]
    public void ApplicationDbContextShouldBeInPersistenceLayer()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespace(Namespace.PersistenceNamespace)
            .Should()
            .HaveName("ApplicationDbContext")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }
}