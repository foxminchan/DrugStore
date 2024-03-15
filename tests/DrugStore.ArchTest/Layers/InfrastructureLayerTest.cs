using DrugStore.ArchTest.Constants;
using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest.Layers;

public sealed class InfrastructureLayerTest
{
    [Fact]
    public void InfrastructureLayerShouldNotHaveDependencyOnAnyLayer()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
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
}