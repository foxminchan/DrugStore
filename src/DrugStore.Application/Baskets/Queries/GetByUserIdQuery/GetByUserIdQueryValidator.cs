using FluentValidation;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed class GetByUserIdQueryValidator : AbstractValidator<GetByUserIdQuery>
{
    public GetByUserIdQueryValidator() => RuleFor(x => x.CustomerId).NotEmpty();
}