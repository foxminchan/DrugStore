using DrugStore.ArchTest.Constants;
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
            Namespace.Domain,
            Namespace.Infrastructure,
            Namespace.Application,
            Namespace.Presentation,
            Namespace.Persistence
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
            .ResideInNamespace(Namespace.Persistence)
            .Should()
            .HaveName("ApplicationDbContext")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }
}