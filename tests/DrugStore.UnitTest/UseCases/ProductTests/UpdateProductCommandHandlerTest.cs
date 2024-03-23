using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class UpdateProductCommandHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly IRepository<Product> _repository = Substitute.For<IRepository<Product>>();

    private readonly ILocalStorage _localStorage = Substitute.For<ILocalStorage>();

    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTest() => _handler = new(_mapper, _repository, _localStorage);

    [Fact]
    public async Task ShouldBeUpdateProductSuccessfully()
    {
        // Arrange
        var command = new UpdateProductCommand(
            new(Guid.NewGuid()),
            "Product Name",
            "Product Code",
            "Product Detail",
            10,
            new(Guid.NewGuid()),
            ProductPriceBuilder.WithDefaultValues(),
            false,
            null,
            null
        );
        _repository.UpdateAsync(Arg.Any<Product>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}