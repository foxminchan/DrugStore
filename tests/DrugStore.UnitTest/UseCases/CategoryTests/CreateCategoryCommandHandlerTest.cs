using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class CreateCategoryCommandHandlerTest
{
    private readonly CreateCategoryCommandHandler _handler;

    private readonly ILogger<CreateCategoryCommandHandler> _logger =
        Substitute.For<ILogger<CreateCategoryCommandHandler>>();

    private readonly IRepository<Category> _repository = Substitute.For<IRepository<Category>>();

    public CreateCategoryCommandHandlerTest() => _handler = new(_repository, _logger);

    private static Category CreateCategory() => new("Category Name", "Category Description");

    [Fact]
    public async Task ShouldBeCreateCategorySuccessfully()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Guid.NewGuid(),
            "Category Name",
            "Category Description"
        );
        _repository.AddAsync(Arg.Any<Category>())
            .Returns(Task.FromResult(CreateCategory()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}