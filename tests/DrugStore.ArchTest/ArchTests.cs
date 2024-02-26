using FluentAssertions;
using NetArchTest.Rules;

namespace DrugStore.ArchTest;

public class ArchTests
{
    private const string DomainNamespace = "Domain";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string ApplicationNamespace = "Application";
    private const string PresentationNamespace = "Presentation";
    private const string PersistenceNamespace = "Persistence";

    [Fact]
    public void InvokesDomainLayerDependencies()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;
        string[] layers =
        [
            DomainNamespace, 
            InfrastructureNamespace, 
            ApplicationNamespace, 
            PresentationNamespace,
            PersistenceNamespace
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
    public void InvokesInfrastructureLayerDependencies()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
        string[] layers =
        [
            DomainNamespace, 
            InfrastructureNamespace, 
            ApplicationNamespace, 
            PresentationNamespace,
            PersistenceNamespace
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
    public void InvokesApplicationLayerDependencies()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;
        string[] layers =
        [
            DomainNamespace, 
            InfrastructureNamespace, 
            ApplicationNamespace, 
            PresentationNamespace,
            PersistenceNamespace
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
    public void InvokesPresentationLayerDependencies()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;
        string[] layers =
        [
            DomainNamespace, 
            InfrastructureNamespace, 
            ApplicationNamespace, 
            PresentationNamespace,
            PersistenceNamespace
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
    public void InvokesPersistenceLayerDependencies()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;
        string[] layers =
        [
            DomainNamespace, 
            InfrastructureNamespace, 
            ApplicationNamespace, 
            PresentationNamespace,
            PersistenceNamespace
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
    public void RepositoryShouldBeInPersistenceLayer()
    {
        // Arrange
        var assembly = typeof(Persistence.Repository<>).Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .Should()
            .ResideInNamespaceEndingWith(PersistenceNamespace)
            .And()
            .HaveName("Repository")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(result.ToString());
    }
}
