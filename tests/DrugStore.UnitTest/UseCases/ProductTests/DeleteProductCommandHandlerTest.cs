using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence.Repositories;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class DeleteProductCommandHandlerTest
{
    private readonly DeleteProductCommandHandler _handler;

    private readonly ILocalStorage _localStorage = Substitute.For<ILocalStorage>();
    private readonly IRepository<Product> _repository = Substitute.For<IRepository<Product>>();

    public DeleteProductCommandHandlerTest() => _handler = new(_repository, _localStorage);

    [Fact]
    public async Task ShouldBeDeleteProductSuccessfully()
    {
        // Arrange
        var command = new DeleteProductCommand(new(Guid.NewGuid()), false);
        _repository.DeleteAsync(Arg.Any<Product>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}