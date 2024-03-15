using DrugStore.Application.Categories.Commands.CreateCategoryCommand;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.Categories;

public sealed class CreateCategoryCommandHandlerTest
{
    private readonly IRepository<Category> _categoryRepository = Substitute.For<IRepository<Category>>();

    private static Category CreateCategory() => new("Category Name", "Category Description");

    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandHandlerTest() => _handler = new(_categoryRepository);

    [Fact]
    public async Task ShouldBeCreateCategorySuccessfully()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Guid.NewGuid(),
            "Category Name",
            "Category Description"
        );
        _categoryRepository.AddAsync(Arg.Any<Category>())
            .Returns(Task.FromResult(CreateCategory()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}