using FluentValidation;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}
