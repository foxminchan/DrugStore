using DrugStore.Application.Products.Commands.UpdateProductImageCommand;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class UpdateProductImageCommandHandlerTest
{
    private readonly IRepository<Product> _repository = Substitute.For<IRepository<Product>>();

    private readonly ILocalStorage _localStorage = Substitute.For<ILocalStorage>();

    private readonly UpdateProductImageCommandHandler _handler;

    public UpdateProductImageCommandHandlerTest() => _handler = new(_repository, _localStorage);

    [Fact]
    public async Task ShouldBeUpdateProductImageSuccessfully()
    {
        // Arrange
        var command = new UpdateProductImageCommand(new(Guid.NewGuid()), "image.jpg",
            new FormFile(null!, 0, 0, "image.jpg", "image.jpg"));
        _repository.UpdateAsync(Arg.Any<Product>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}