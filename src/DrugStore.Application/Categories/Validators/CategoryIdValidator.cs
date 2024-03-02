using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Persistence;
using FluentValidation;

namespace DrugStore.Application.Categories.Validators;

public sealed class CategoryIdValidator : AbstractValidator<CategoryId?>
{
    private readonly Repository<Category> _repository;

    public CategoryIdValidator(Repository<Category> repository)
    {
        _repository = repository;
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateId);
    }

    private async Task<bool> ValidateId(CategoryId? id, CancellationToken cancellation)
        => id is null || await _repository.GetByIdAsync(id.Value, cancellation) is { };
}