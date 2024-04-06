using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence.Repositories;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class CreateProductCommandHandlerTest
{
    private readonly CreateProductCommandHandler _handler;
    private readonly ILocalStorage _localStorage = Substitute.For<ILocalStorage>();

    private readonly ILogger<CreateProductCommandHandler> _logger =
        Substitute.For<ILogger<CreateProductCommandHandler>>();

    private readonly IRepository<Product> _repository = Substitute.For<IRepository<Product>>();

    public CreateProductCommandHandlerTest() => _handler = new(_repository, _logger, _localStorage);

    private static Product CreateProduct() => new(
        "Product Name",
        "Product Code",
        "Product Detail",
        10,
        new(Guid.NewGuid()),
        ProductPriceBuilder.WithDefaultValues()
    );

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
            ProductPriceBuilder.WithDefaultValues(),
            null,
            null
        );
        _repository.AddAsync(Arg.Any<Product>())
            .Returns(Task.FromResult(CreateProduct()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}