using DrugStore.Domain.Category;
using DrugStore.Persistence;
using FluentValidation;

namespace DrugStore.Application.Products.Validators;

public sealed class ProductIdValidator : AbstractValidator<Guid?>
{
    private readonly Repository<Category> _repository;

    public ProductIdValidator(Repository<Category> repository)
    {
        _repository = repository;
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateId);
    }

    private async Task<bool> ValidateId(Guid? id, CancellationToken cancellation)
        => id is null || await _repository.GetByIdAsync(id.Value, cancellation) is { };
}
