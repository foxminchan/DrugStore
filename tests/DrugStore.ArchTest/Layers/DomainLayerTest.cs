using DrugStore.ArchTest.Constants;
using DrugStore.Domain;
using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest.Layers;

public sealed class DomainLayerTest
{
    [Fact]
    public void DomainLayerShouldNotHaveDependencyOnAnyLayer()
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
}