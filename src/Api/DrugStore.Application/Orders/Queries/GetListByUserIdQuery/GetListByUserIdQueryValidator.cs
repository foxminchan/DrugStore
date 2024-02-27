using FluentValidation;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed class GetListByUserIdQueryValidator : AbstractValidator<GetListByUserIdQuery>
{
    public GetListByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Filter.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThan(0);
    }
}
