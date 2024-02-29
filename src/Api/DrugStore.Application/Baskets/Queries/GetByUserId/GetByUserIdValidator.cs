using FluentValidation;

namespace DrugStore.Application.Baskets.Queries.GetByUserId;

public sealed class GetByUserIdValidator : AbstractValidator<GetByUserId>
{
    public GetByUserIdValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Filter.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThan(0);
    }
}
