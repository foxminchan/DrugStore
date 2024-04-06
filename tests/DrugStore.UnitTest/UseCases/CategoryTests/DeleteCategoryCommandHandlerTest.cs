using DrugStore.Application.Categories.Commands.DeleteCategoryCommand;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class DeleteCategoryCommandHandlerTest
{
    private readonly IRepository<Category> _repository = Substitute.For<IRepository<Category>>();

    public DeleteCategoryCommandHandlerTest()
    {
        var category = new Category("Category Name", "Category Description");

        _repository.FirstOrDefaultAsync(Arg.Any<CategoryByIdSpec>())
            .Returns(category);
    }

    [Fact]
    public async Task ShouldBeDeleteCategorySuccessfully()
    {
        // Arrange
        var command = new DeleteCategoryCommand(new(Guid.Empty));
        var handler = new DeleteCategoryCommandHandler(_repository);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}