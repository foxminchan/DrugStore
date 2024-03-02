using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Persistence;
using FluentValidation;

namespace DrugStore.Application.Products.Validators;

public sealed class ProductIdValidator : AbstractValidator<ProductId>
{
    private readonly Repository<Category> _repository;

    public ProductIdValidator(Repository<Category> repository)
    {
        _repository = repository;
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(ValidateId);
    }

    private async Task<bool> ValidateId(ProductId id, CancellationToken cancellation)
        => await _repository.GetByIdAsync(id.Value, cancellation) is { };
}