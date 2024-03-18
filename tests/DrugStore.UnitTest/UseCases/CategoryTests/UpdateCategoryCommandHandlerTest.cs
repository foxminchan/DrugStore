using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class UpdateCategoryCommandHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IRepository<Category> _repository = Substitute.For<IRepository<Category>>();

    private static Category CreateCategory() => new("Category Name", "Category Description");

    private readonly UpdateCategoryCommandHandler _handler;

    public UpdateCategoryCommandHandlerTest() => _handler = new(_mapper, _repository);

    [Fact]
    public async Task ShouldBeUpdateCategorySuccessfully()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            new(Guid.NewGuid()),
            "Category Name",
            "Category Description"
        );
        _repository.FirstOrDefaultAsync(Arg.Any<CategoryByIdSpec>())!
            .Returns(Task.FromResult(CreateCategory()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}