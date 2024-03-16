using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class CreateProductCommandHandlerTest
{
    private readonly IRepository<Product> _repository = Substitute.For<IRepository<Product>>();

    private static Product CreateProduct() => new(
        "Product Name",
        "Product Code",
        "Product Detail",
        10,
        new(Guid.NewGuid()),
        ProductPriceBuilder.WithDefaultValues()
    );

    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTest() => _handler = new(_repository);

    [Fact]
    public async Task ShouldBeCreateProductSuccessfully()
    {
        // Arrange
        var command = new CreateProductCommand(
            Guid.NewGuid(),
            "Product Name",
            "Product Code",
            "Product Detail",
            10,
            new(Guid.NewGuid()),
            ProductPriceBuilder.WithDefaultValues()
        );
        _repository.AddAsync(Arg.Any<Product>())
            .Returns(Task.FromResult(CreateProduct()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}