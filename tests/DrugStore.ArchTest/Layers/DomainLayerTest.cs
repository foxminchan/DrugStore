using DrugStore.ArchTest.Fixtures;
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
}